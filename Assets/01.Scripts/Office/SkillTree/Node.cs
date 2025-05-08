using GGM.UI;
using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Office.CharacterSkillTree
{
    public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private NodeSO _nodeType;

        [SerializeField] private Image _disablePanel;
        [SerializeField] private UILineRenderer _edge;
        [SerializeField] private UILineRenderer _edgeFill;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _vertexFill;
        [SerializeField] private float _enableTime = 0.5f;
        [SerializeField] private Material _lineMaterial;

        private Vector2[] _offsets;
        private bool _isNodeEnable;
        private bool _isNodeActive = true;
        private Node _curEnableNode;

        private Stack<Node> _prevNodes;
        private Stack<Node> _enabledNodes;
        private Coroutine _enableCoroutine;
        private Coroutine _currentEnableRoutine;

        private Coroutine _cancelCoroutine;
        private Coroutine _currentCancelRoutine;

        private CharacterEnum _characterType;
        private readonly string CoinLackText = $"코인이 부족합니다ㅠㅠ";

        #region Property

        private SkillTree techTree => GetComponentInParent<SkillTree>();
        public RectTransform RectTrm => transform as RectTransform;
        public bool IsNodeEnable => _isNodeEnable;
        public NodeSO NodeType => _nodeType;

        #endregion


        private void Awake()
        {
            _prevNodes = new Stack<Node>();
            InitEdge();

            _icon.gameObject.SetActive(NodeType.icon != null);
            _icon.sprite = NodeType.icon;
        }

        private void OnEnable()
        {
            _edge.gameObject.SetActive(true);
            _edgeFill.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _edge.gameObject.SetActive(false);
            _edgeFill.gameObject.SetActive(false);
        }

        #region EnableNode

        //선택된 노드들을 전부 활성화시켜주는 코루틴
        public IEnumerator StartEnableAllSelectedNodes()
        {
            if (_cancelCoroutine != null)
                StopCoroutine(_cancelCoroutine);
            if (_currentCancelRoutine != null)
                StopCoroutine(_currentCancelRoutine);

            GetPrevNodes();
            _enabledNodes = new Stack<Node>();

            //_prevNodes에 활성화 시킬 노드들을 순차적으로 꺼내서 활성화시키기 시도
            while (_prevNodes.TryPop(out _curEnableNode))
            {
                _currentEnableRoutine = StartCoroutine(_curEnableNode.EnableNodeRoutine());
                _enabledNodes.Push(_curEnableNode);
                yield return _currentEnableRoutine;
            }

            //전부 활성화 됬다면, 한번에 데이터상에서 활성화 시켜주기
            //취소했을 때 전부 사라져야하기 때문
            while (_enabledNodes.TryPop(out _curEnableNode))
            {
                _curEnableNode.EnableNode();
            }
        }

        public IEnumerator EnableNodeRoutine()
        {
            float process = 0;

            if (_nodeType is not StartNodeSO)
            {
                _edgeFill.transform.SetAsLastSibling();
                while (process < 1)
                {
                    process += Time.deltaTime / (_enableTime / 2);
                    _edgeFill.SetFillAmount(process);
                    yield return null;
                }
            }

            process = 0;
            while (process < 1)
            {
                process += Time.deltaTime / (_enableTime / 2);
                _vertexFill.fillAmount = process;
                yield return null;
            }
        }

        public void EnableNode(bool isLoading = false)
        {
            _isNodeEnable = true;
            //재화 사용
            //.Instance.(NodeType.requireCoin);

            //fillAmount 1로 초기화
            _vertexFill.fillAmount = 1;
            _edgeFill.SetFillAmount(1);

            NodeType.exceptNodes.ForEach(exceptNode => techTree.GetNode(exceptNode.id).SetActive(false));

            //어떤 스탯이 열렸는지만 저장
            if (isLoading == false)
                SaveManager.Instance.AddSaveStat(_characterType, NodeType);
        }

        #endregion


        #region CancelEnableNode

        public IEnumerator CancelEnableNode()
        {
            if (_enableCoroutine != null)
                StopCoroutine(_enableCoroutine);
            if (_currentEnableRoutine != null)
                StopCoroutine(_currentEnableRoutine);

            while (_enabledNodes.TryPop(out _curEnableNode))
            {
                _currentCancelRoutine = StartCoroutine(_curEnableNode.DisableNodeRoutine());
                yield return _currentCancelRoutine;
            }
        }

        public IEnumerator DisableNodeRoutine()
        {
            float process = _vertexFill.fillAmount;

            while (process > 0)
            {
                process -= Time.deltaTime / (_enableTime / 2);
                _vertexFill.fillAmount = process;
                yield return null;
            }

            process = _edgeFill.GetFillAmount();
            if (_nodeType is not StartNodeSO)
            {
                while (process > 0)
                {
                    process -= Time.deltaTime / (_enableTime / 2);
                    _edgeFill.SetFillAmount(process);
                    yield return null;
                }
            }
        }

        #endregion


        private void GetPrevNodes()
        {
            _prevNodes = new Stack<Node>();
            if (IsNodeEnable == false)
            {
                int requireCoin = 0;
                NodeSO curNode = _nodeType;

                while (curNode != null && !techTree.GetNode(curNode.id).IsNodeEnable)
                {
                    requireCoin += curNode.requireCoin;
                    _prevNodes.Push(techTree.GetNode(curNode.id));

                    curNode = curNode.prevNode;
                }

                //int coin = GameDataManager.Instance.Coin;
                //_techTree.selectNodeEvent?.Invoke(coin, requireCoin);
            }
        }

        public void SetActive(bool isActive)
        {
            _isNodeActive = isActive;
            NodeType.nextNodes.ForEach(next => techTree.GetNode(next.id).SetActive(isActive));

            if (isActive) ActiveNode();
            else UnActiveNode();
        }

        private void ActiveNode()
        {
            _disablePanel.color = new Color(1f, 1f, 1f, 0f);
        }

        private void UnActiveNode()
        {
            _disablePanel.color = new Color(0f, 0f, 0f, 0.9f);
        }


        public void Init(CharacterEnum characterType)
        {
            _characterType = characterType;
        }


        #region Edge

        private void InitEdge()
        {
            _edge.transform.SetParent(techTree.edgeParent);
            _edgeFill.transform.SetParent(techTree.edgeFillParent);

            _edge.SetMaterial(_lineMaterial);
            _edgeFill.SetMaterial(_lineMaterial);
        }

        public void SetEdge()
        {
            if (_nodeType.prevNode == null) return;

            Vector2 size = RectTrm.sizeDelta;
            Node prevNode = techTree.GetNode(_nodeType.prevNode.id);

            _offsets = new Vector2[4]
            {
            //down
            new Vector2(size.x * 0.5f, 0),
            //up
            new Vector2(0, size.y * 0.5f),
            //left
            new Vector2(0, size.y * 0.5f),
            //right
            new Vector2(size.x * 0.5f, 0)
            };


            Vector3 startPosition = _offsets[0];
            Vector3 relativePosition = transform.InverseTransformPoint(prevNode.transform.position) + (Vector3)_offsets[1];
            Vector3 delta = relativePosition - startPosition;

            if (prevNode.RectTrm.anchoredPosition.y < RectTrm.anchoredPosition.y - size.y / 2)
            {
                startPosition = _offsets[0];
                relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[1]);
                delta += new Vector3(size.x * 0.5f, size.y * 0.5f);
            }
            else if (prevNode.RectTrm.anchoredPosition.y > RectTrm.anchoredPosition.y + size.y / 2)
            {
                startPosition = _offsets[1];
                relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[0]);
                delta += new Vector3(size.x * 0.5f, 0);
            }
            else
            {
                if (prevNode.RectTrm.anchoredPosition.x < RectTrm.anchoredPosition.x - size.x / 2)
                {
                    startPosition = _offsets[2];
                    relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[3]);
                    delta += new Vector3(0f, size.y * 0.5f);
                }
                else if (prevNode.RectTrm.anchoredPosition.x > RectTrm.anchoredPosition.x + size.x / 2)
                {
                    startPosition = _offsets[3];
                    relativePosition = transform.InverseTransformPoint(prevNode.transform.position + (Vector3)_offsets[2]);
                    delta += new Vector3(0f, size.y * 0.5f);
                }
            }


            Vector3 endPosition = startPosition + delta;
            Vector3 middlePosition = delta * 0.5f + startPosition;

            _edge.points = new Vector2[]
                    {
                    endPosition,
                    new Vector2(endPosition.x, middlePosition.y),
                    new Vector2(startPosition.x, middlePosition.y),
                    startPosition
                    };

            _edgeFill.points = new Vector2[]
                    {
                    endPosition,
                    new Vector2(endPosition.x, middlePosition.y),
                    new Vector2(startPosition.x, middlePosition.y),
                    startPosition
                    };

            _edge.SetFillAmount(1);
            _edgeFill.SetFillAmount(0);
        }

        public void SetNode(NodeSO node)
        {
            _nodeType = node;
            _icon.sprite = node.icon;
        }

        #endregion


        #region InputRegion


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isNodeActive == false) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;


        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isNodeActive == false) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;
            _prevNodes = new Stack<Node>();

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isNodeActive == false) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            //if (_requireCoin > GameDataManager.Instance.Coin) return;

            _enableCoroutine = StartCoroutine(StartEnableAllSelectedNodes());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isNodeActive == false) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            //if (_requireCoin > GameDataManager.Instance.Coin) return;

            _cancelCoroutine = StartCoroutine(CancelEnableNode());
        }

        #endregion
    }

}
