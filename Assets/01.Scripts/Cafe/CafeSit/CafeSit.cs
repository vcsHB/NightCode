using Agents.Players;
using Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    public class CafeSit : BaseInteractiveObject
    {
        public Transform customerPosition;

        [SerializeField] private Transform _playerPosition;
        [SerializeField] private SpriteRenderer _iconRenderer;
        [SerializeField] private float _playerStandingOffset;

        [SerializeField] private Sprite _onSitIcon, _requierFoodIcon, _dirtyIcon;
        private List<Sprite> _interactIcon;

        private bool _isGetFood;
        private bool _isInteractive;
        private CafeSitStateMachine _stateMachine;

        public CafeCustomer AssingedCustomer { get; private set; }
        public AvatarPlayer Player => _player;
        public bool IsInteractive => _isInteractive;
        public bool IsGetFood => _isGetFood;


        private void Awake()
        {
            _stateMachine = new CafeSitStateMachine();
            _stateMachine.AddState(ECafeSitState.Empty, new CafeSitEmptyState(this));
            _stateMachine.AddState(ECafeSitState.Wait, new CafeSitWaitState(this));
            _stateMachine.AddState(ECafeSitState.Dirty, new CafeSitDirtyState(this));
            _stateMachine.ChangeState(ECafeSitState.Empty);

            _interactIcon = new List<Sprite>(3)
            {
                _onSitIcon,
                _requierFoodIcon,
                _dirtyIcon
            };
        }


        //�����ͻ� �մ� �Ҵ�
        public void SetCustomer(CafeCustomer customer)
        {
            AssingedCustomer = customer;
            _isInteractive = customer.customerSO.isInteractiveCustomer;
        }

        //���ӿ��� �մ� �Ҵ�
        public void CustomerSit()
        {
            _stateMachine.ChangeState(ECafeSitState.Wait);
        }


        #region FOR_FSM

        public void ServeByPlayer()
        {
            _player.SetMoveTarget(CalculatePlayerPosition(_player.transform));
            _player.onCompleteMove += OnCompleteServeByPlayer;
        }

        private void OnCompleteServeByPlayer()
        {
            SetInteractIcon(ECafeSitIcon.FoodIcon, false);
            AssingedCustomer.GetFood();

            _player.ServeFood();
            _player.onCompleteMove -= OnCompleteServeByPlayer;
        }

        [Obsolete]
        public void ServeByNPC()
        {
            Debug.LogWarning("Serving by npc is not valid");
            //SetInteractIcon(ECafeSitIcon.OrderIcon, false);
            //AssingedCustomer.GetFood();
            //_isCustomerWaitingMenu = false;
        }

        //�մ��� ������
        public void LeaveCustomer()
        {
            if (AssingedCustomer == null) return;

            AssingedCustomer = null;
            _stateMachine.ChangeState(ECafeSitState.Dirty);
        }

        public void CleanTable()
        {
            _stateMachine.ChangeState(ECafeSitState.Empty);
        }


        #endregion


        public Transform CalculatePlayerPosition(Transform playerTrm)
        {
            float direction = Mathf.Sign(playerTrm.position.x - transform.position.x);
            _playerPosition.position = transform.position + new Vector3(_playerStandingOffset * direction, 0, 0);
            return _playerPosition;
        }


        public void SetInteractIcon(ECafeSitIcon iconType, bool isEnable)
        {
            _iconRenderer.gameObject.SetActive(isEnable);
            _iconRenderer.sprite = _interactIcon[(int)iconType];
        }

        public override void OnPlayerInteract()
        {
            _stateMachine.OnTriggerEnter();
        }

        public override void OnPlayerInteractExit()
        {
            _stateMachine.OnTriggerExit();
        }
    }

    public enum ECafeSitIcon
    {
        OrderIcon,
        FoodIcon,
        CleanIcon
    }
}
