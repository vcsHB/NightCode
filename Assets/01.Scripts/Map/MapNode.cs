using Core.Attribute;
using DG.Tweening;
using GGM.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<MapNodeSO> onSelectNode;

        [SerializeField] private GameObject _completePanel;
        [SerializeField] private List<Image> _coloredImages;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nodeName;
        [SerializeField] private NodeEdge _leftEdge, _rightEdge;
        private MapNodeSO _nodeInfo;

        public RectTransform RectTrm => transform as RectTransform;
        public NodeEdge LeftEdge => _leftEdge;


        public void Init(MapNodeSO nodeInfo)
        {
            _nodeInfo = nodeInfo;
            _icon.sprite = nodeInfo.icon;
            _nodeName.SetText(nodeInfo.displayName);
            _coloredImages.ForEach(img => img.color = nodeInfo.color);
            _completePanel.SetActive(false);
        }

        public void ConnectEdge(MapNode target)
        {
            RectTransform from = _rightEdge.edgeObject.transform as RectTransform;
            RectTransform to = target.LeftEdge.edgeObject.transform as RectTransform;

            Vector2 diff = target.RectTrm.anchoredPosition - RectTrm.anchoredPosition;
            diff -= (from.anchoredPosition - RectTrm.anchoredPosition) * 2;

            _rightEdge.edgeLine.points = new Vector2[4];
            _rightEdge.edgeLine.points[0] = Vector2.zero;
            _rightEdge.edgeLine.points[1] = new Vector2(diff.x / 2, 0);
            _rightEdge.edgeLine.points[2] = new Vector2(diff.x / 2, diff.y);
            _rightEdge.edgeLine.points[3] = diff;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onSelectNode?.Invoke(_nodeInfo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }
    }

    [Serializable]
    public struct NodeEdge
    {
        public GameObject edgeObject;
        public UILineRenderer edgeLine;
    }
}
