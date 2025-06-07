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
        [SerializeField] private Transform _leftEdge, _rightEdge;
        [SerializeField] private UILineRenderer _edgePrefab;
        private MapNodeSO _nodeInfo;
        private Color _nodeColor;

        public RectTransform RectTrm => transform as RectTransform;
        public Transform LeftEdge => _leftEdge;
        public Color NodeColor => _nodeColor;


        public void Init(MapNodeSO nodeInfo)
        {
            _nodeInfo = nodeInfo;
            _icon.sprite = nodeInfo.icon;
            _nodeName.SetText(nodeInfo.displayName);
            _coloredImages.ForEach(img => img.color = nodeInfo.color);
            _nodeColor = nodeInfo.color;
            _completePanel.SetActive(false);
        }

        public void ConnectEdge(MapNode target)
        {
            RectTransform to = _rightEdge as RectTransform;
            RectTransform from = target.LeftEdge as RectTransform;

            Vector2 diff = RectTrm.anchoredPosition - target.RectTrm.anchoredPosition;
            diff -= from.anchoredPosition;
            diff += to.anchoredPosition;

            UILineRenderer lineRenderer = Instantiate(_edgePrefab, target.LeftEdge);
            lineRenderer.SetMaterial(lineRenderer.material);

            lineRenderer.SetColor(NodeColor, target.NodeColor);

            lineRenderer.points = new Vector2[4];
            lineRenderer.points[0] = diff;
            lineRenderer.points[1] = new Vector2(diff.x - (diff.x * 0.2f), diff.y);
            lineRenderer.points[2] = new Vector2(diff.x * 0.2f, 0);
            lineRenderer.points[3] = Vector2.zero;
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
        public List<UILineRenderer> edgeLine;

        public void AddEdge(UILineRenderer lineRenderer)
        {
            if (edgeLine == null)
                edgeLine = new List<UILineRenderer>();

            edgeLine.Add(lineRenderer);
        }
    }
}
