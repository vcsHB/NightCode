using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class MapCharacterController : MonoBehaviour
    {
        [SerializeField] private Transform _lineParent;
        [SerializeField] private MapCharacterIcon _characterIconPrefab;

        private Dictionary<CharacterEnum, MapCharacterIcon> _characterIcons = new Dictionary<CharacterEnum, MapCharacterIcon>();

        private MapNode _selectedNode;
        private MapNode _prevSelectedNode;

        private ScrollRect _scrollRect;
        private MapGraph _mapGraph;

        public void Init(MapGraph mapGraph)
        {
            _mapGraph = mapGraph;
            _scrollRect = GetComponentInChildren<ScrollRect>();
            SetCharacters();
        }

        public MapCharacterIcon GetIcon(CharacterEnum character) => _characterIcons[character];

        private void SetCharacters()
        {
            //시작 노드 제외 현재 맵들을 다 클리어해야 진행가능
            bool isComplete = true;
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_mapGraph.IsCharacterExsists(character))
                {
                    Vector2Int position = _mapGraph.GetCharcterCurrentPosition(character);
                    if (_mapGraph.GetNode(position).IsComplete == false) isComplete = false;
                }
            }

            int minX = int.MaxValue;
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_mapGraph.IsCharacterExsists(character) == false) continue;

                Vector2Int position = _mapGraph.GetCharacterOriginPosition(character);
                minX = Mathf.Min(minX, position.x);
                MapNode node = _mapGraph.GetNode(position);
                node.SetSelectObjectEnable(true);

                MapCharacterIcon characterIcon = Instantiate(_characterIconPrefab, node.CharacterIconParent);
                characterIcon.SetCharacter(character);
                characterIcon.onPointerUp += MoveCharacter;
                characterIcon.onReturnOriginNode += ReturnCharacterIconOriginPosition;
                characterIcon.SetCompleteCurrentLevel(isComplete);

                node.characterIcons.Add(characterIcon);
                _characterIcons.Add(character, characterIcon);
            }

            _scrollRect.content.anchoredPosition = new Vector2(minX * -600f, 0f);
        }

        private void ReturnCharacterIconOriginPosition(MapCharacterIcon icon)
        {
            Vector2Int originPosition = _mapGraph.GetCharcterCurrentPosition(icon.Character);
            MapNode originNode = _mapGraph.GetNode(originPosition);

            Vector2Int targetPosition = _mapGraph.GetCharacterOriginPosition(icon.Character);
            MapNode targetNode = _mapGraph.GetNode(targetPosition);
            originNode.RemoveIcon(icon);
            targetNode.AddIcon(icon);

            icon.SetMoved(false);
            icon.SetParent(targetNode.CharacterIconParent);
            _mapGraph.MoveCharacter(icon.Character, targetNode.Position);
            targetNode.SetSelectObjectEnable(true);

            if (_mapGraph.CheckConnectionExsist(targetPosition, originPosition) == false)
            {
                targetNode.SetConnectionLine(originNode, false);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(targetNode.CharacterIconParent);

            if (_mapGraph.IsCurrentPositionExsist(originPosition) == false)
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
                Vector2Int originPosition = _mapGraph.GetCharacterOriginPosition(icon.Character);
                MapNode originNode = _mapGraph.GetNode(originPosition);

                originNode.RemoveIcon(icon);
                _selectedNode.AddIcon(icon);

                // 이동 가능한 노드인지 체크해야함
                bool containOriginNode = _selectedNode.NodeInfo.prevNodes.Contains(originNode.NodeInfo);
                if (containOriginNode == false)
                {
                    ReturnToPrevPosition(icon.Character);
                    return;
                }

                icon.SetMoved(true);
                icon.SetParent(_selectedNode.CharacterIconParent);
                _mapGraph.MoveCharacter(icon.Character, _selectedNode.Position);

                _selectedNode.SetSelectObjectEnable(true);
                originNode.SetConnectionLine(_selectedNode, true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(_selectedNode.CharacterIconParent);

                //when originNode does not contain any character turn off select object
                if (_mapGraph.IsCurrentPositionExsist(originPosition) == false)
                    originNode.SetSelectObjectEnable(false);
            }
        }

        private void ReturnToPrevPosition(CharacterEnum character)
        {
            Vector2Int position = _mapGraph.GetCharacterOriginPosition(character);
            MapNode originMap = _mapGraph.GetNode(position);

            LayoutRebuilder.ForceRebuildLayoutImmediate(originMap.CharacterIconParent);
        }

        public void HandleSelectCharacterIcon(MapNode node)
        {
            if (node == null)
            {
                _selectedNode = null;
                _prevSelectedNode = null;
                return;
            }

            //여기도 고쳐야하는
            if (node.characterIcons.Count > 0 && node.characterIcons[0].IsMoved == false)
            {
                if (_prevSelectedNode != null)
                {
                    _prevSelectedNode.nextPositions.ForEach(next =>
                    {
                        MapNode nextMapNode = _mapGraph.GetNode(next);
                        if (nextMapNode != null) _prevSelectedNode.SetConnectableLine(nextMapNode, true);
                    });
                }

                node.nextPositions.ForEach(next =>
                {
                    MapNode nextMapNode = _mapGraph.GetNode(next);
                    if (nextMapNode != null) node.SetConnectableLine(nextMapNode, true);

                });

                _prevSelectedNode = node;
            }

            _selectedNode = node;
        }
    }
}
