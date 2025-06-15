using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Map
{
    public class CharacterIconController : MonoBehaviour
    {
        [SerializeField] private Transform _lineParent;
        [SerializeField] private MapCharacterIcon _characterIconPrefab;

        private Dictionary<CharacterEnum, MapCharacterIcon> _characterIcons = new Dictionary<CharacterEnum, MapCharacterIcon>();
        private MapGraph _mapGraph;
        private MapController _mapController;
        private MapNode _selectedNode;

        private void Awake()
        {
            _mapController = GetComponent<MapController>();
            _mapGraph = GetComponent<MapGraph>();

            _mapController.Load();
            _mapGraph.Init();
            Init();
        }

        public void Init()
        {
            SetCharacters();
            _mapGraph.OnPointerUpNodeEvent += HandleSelectCharacterIcon;
        }

        private void SetCharacters()
        {
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                Vector2Int position = _mapController.GetCharacterOriginPosition(character);
                MapNode node = _mapGraph.GetNode(position);
                node.SetSelectObjectEnable(true);

                MapCharacterIcon characterIcon = Instantiate(_characterIconPrefab, node.CharacterIconParent);
                characterIcon.SetCharacter(character);
                characterIcon.onPointerUp += MoveCharacter;
                characterIcon.onReturnOriginNode += ReturnCharacterIconOriginPosition;

                _characterIcons.Add(character, characterIcon);
            }
        }

        private void ReturnCharacterIconOriginPosition(MapCharacterIcon icon)
        {
            Vector2Int originPosition = _mapController.GetCharcterCurrentPosition(icon.Character);
            MapNode originNode = _mapGraph.GetNode(originPosition);

            Vector2Int targetPosition = _mapController.GetCharacterOriginPosition(icon.Character);
            MapNode targetNode = _mapGraph.GetNode(targetPosition);

            icon.SetMoved(false);
            icon.SetParent(targetNode.CharacterIconParent);
            _mapController.MoveCharacter(icon.Character, targetNode.Position);
            targetNode.SetSelectObjectEnable(true);

            if (_mapController.CheckConnectionExsist(targetPosition, originPosition) == false)
            {
                targetNode.SetConnectionLine(originNode, false);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(targetNode.CharacterIconParent);

            if (_mapController.IsCurrentPositionExsist(originPosition) == false)
                originNode.SetSelectObjectEnable(false);
        }

        private void MoveCharacter(MapCharacterIcon icon)
        {
            if (_selectedNode == null)
            {
                //원래 위치로 돌아가기
                ReturnToPrevPosition(icon.Character);
            }
            else
            {
                //이동 시키기
                Vector2Int originPosition = _mapController.GetCharacterOriginPosition(icon.Character);
                MapNode originNode = _mapGraph.GetNode(originPosition);

                // 이동 가능한 노드인지 체크해야함
                bool containOriginNode = _selectedNode.NodeInfo.prevNodes.Contains(originNode.NodeInfo);
                if (containOriginNode == false)
                {
                    ReturnToPrevPosition(icon.Character);
                    return;
                }

                icon.SetMoved(true);
                icon.SetParent(_selectedNode.CharacterIconParent);
                _mapController.MoveCharacter(icon.Character, _selectedNode.Position);

                _selectedNode.SetSelectObjectEnable(true);
                originNode.SetConnectionLine(_selectedNode, true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(_selectedNode.CharacterIconParent);

                //when originNode does not contain any character turn off select object
                if (_mapController.IsCurrentPositionExsist(originPosition) == false)
                    originNode.SetSelectObjectEnable(false);

                //이건 맵 이동을 완료시키는 뭔가 다른 무언가를 해서 그 쪽으로 이동 시켜야함
                bool moveComplete = true;
                foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
                {
                    if (_mapController.IsCharacterMoved(character) == false)
                        moveComplete = false;
                }
                if (moveComplete) _mapController.Save();
            }
        }

        private void ReturnToPrevPosition(CharacterEnum character)
        {
            Vector2Int position = _mapController.GetCharacterOriginPosition(character);
            MapNode originMap = _mapGraph.GetNode(position);

            LayoutRebuilder.ForceRebuildLayoutImmediate(originMap.CharacterIconParent);
        }

        private void HandleSelectCharacterIcon(MapNode node)
        {
            if (node == null)
            {
                _selectedNode = null;
                return;
            }

            if (_selectedNode != null)
            {
                _selectedNode.NodeInfo.nextNodes.ForEach(nextNode =>
                {
                    MapNode nextMapNode = _mapGraph.GetNode(nextNode);
                    if (nextMapNode != null) _selectedNode.SetConnectableLine(nextMapNode, false);
                });
            }

            if (node.Depth == _mapController.CurrentDepth)
            {
                node.NodeInfo.nextNodes.ForEach(nextNode =>
                {
                    MapNode nextMapNode = _mapGraph.GetNode(nextNode);
                    if (nextMapNode != null) node.SetConnectableLine(nextMapNode, true);
                });
            }

            _selectedNode = node;
        }
    }
}
