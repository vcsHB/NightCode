using GGM.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Map
{
    public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<MapNode> onSelectNode;
        public event Action<MapNode> onPointerEnter;

        [SerializeField] private CanvasGroup _completePanel;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nodeName;
        [SerializeField] private Transform _leftEdge, _rightEdge;
        [SerializeField] private List<Image> _coloredImages;

        [Space]
        [SerializeField] private GameObject _selectedObject;
        [SerializeField] private RectTransform _characterIcon;

        [Header("Prefabs")]
        [SerializeField] private UILineRenderer _edgePrefab;

        private Dictionary<MapNode, UILineRenderer> _connectionLines;
        private int _depth, _height;
        private MapNodeSO _nodeInfo;
        private Color _nodeColor;

        private bool _isComplete = false;
        public List<Vector2Int> nextPositions;
        [HideInInspector] public List<MapCharacterIcon> characterIcons;


        #region PropertyRegion

        public int Height => _height;
        public int Depth => _depth;
        public Vector2Int Position => new Vector2Int(_depth, _height);
        public RectTransform CharacterIconParent => _characterIcon;
        public MapNodeSO NodeInfo => _nodeInfo;
        public RectTransform RectTrm => transform as RectTransform;
        public Transform LeftEdge => _leftEdge;
        public Color NodeColor => _nodeColor;
        public bool IsComplete => _isComplete;

        #endregion

        public void Init(MapNodeSO nodeInfo, int depth, int height)
        {
            _nodeInfo = nodeInfo;
            _icon.sprite = nodeInfo.icon;
            _nodeName.SetText(nodeInfo.displayName);
            _coloredImages.ForEach(img => img.color = nodeInfo.color);
            _nodeColor = nodeInfo.color;
            _depth = depth;
            _height = height;

            characterIcons = new List<MapCharacterIcon>();
            _leftEdge.gameObject.SetActive(nodeInfo.prevNodes.Count > 0);
            _rightEdge.gameObject.SetActive(nodeInfo.nextNodes.Count > 0);
        }

        public void ConnectEdge(MapNode target, Transform lineParent)
        {
            if (_connectionLines == null) _connectionLines = new Dictionary<MapNode, UILineRenderer>();
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

            lineRenderer.transform.parent = lineParent;
            _connectionLines.Add(target, lineRenderer);
        }

        public void SetSelectObjectEnable(bool isEnable) => _selectedObject.SetActive(isEnable);

        public void CompleteNode()
        {
            _isComplete = true;
            _completePanel.alpha = 1f;
            _completePanel.blocksRaycasts = true;
        }

        public void SetConnectionLine(MapNode to, bool isEnable)
        {
            _connectionLines[to].SetConnection(isEnable);
        }

        public void SetConnectableLine(MapNode to, bool isEnable)
        {
            _connectionLines[to].SetConnectable(isEnable);
        }

        public void AddIcon(MapCharacterIcon icon) => characterIcons.Add(icon);
        public void RemoveIcon(MapCharacterIcon icon)
        {
            if (characterIcons.Contains(icon))
            {
                characterIcons.Remove(icon);
            }
        }

        #region EventRegion

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selectedObject.activeSelf == false) return;
            onSelectNode?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //onPointerEnter?.Invoke(null);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(this);
        }

        #endregion
    }
}
