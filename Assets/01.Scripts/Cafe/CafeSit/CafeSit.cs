using Agents.Players;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class CafeSit : MonoBehaviour
    {
        public Transform customerPosition;

        [SerializeField] private Transform _playerPosition;
        [SerializeField] private SpriteRenderer _iconRenderer;
        [SerializeField] private float _playerStandingOffset;

        [SerializeField] private Sprite _onSitIcon, _requierFoodIcon, _dirtyIcon;
        private List<Sprite> _interactIcon;

        private bool _isGetFood;
        private bool _isInteractive;
        private CafePlayer _player;
        private CafeSitStateMachine _stateMachine;

        public CafeCustomer AssingedCustomer { get; private set; }
        public CafePlayer Player => _player;
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


        //데이터상 손님 할당
        public void SetCustomer(CafeCustomer customer)
        {
            AssingedCustomer = customer;
            _isInteractive = customer.customerSO.isInteractiveCustomer;
        }

        //게임에서 손님 할당
        public void CustomerSit()
        {
            _stateMachine.ChangeState(ECafeSitState.Wait);
        }


        #region FOR_FSM

        public void ServeByPlayer()
        {
            float direction = Mathf.Sign(_player.transform.position.x - transform.position.x);

            _playerPosition.position = transform.position + new Vector3(_playerStandingOffset * direction, 0, 0);
            _player.SetMoveTarget(_playerPosition);
            _player.onCompleteMove += OnCompleteServeByPlayer;
        }

        private void OnCompleteServeByPlayer()
        {
            SetInteractIcon(ECafeSitIcon.FoodIcon, false);
            AssingedCustomer.GetFood();

            _player.ServeFood();
            _player.onCompleteMove -= OnCompleteServeByPlayer;
        }

        public void ServeByNPC(CafeMaid maid)
        {
            _iconRenderer.gameObject.SetActive(false);
            AssingedCustomer.GetFood();
            //_isCustomerWaitingMenu = false;
        }

        //손님이 떠날때
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


        public void SetInteractIcon(ECafeSitIcon iconType, bool isEnable)
        {
            _iconRenderer.gameObject.SetActive(isEnable);
            _iconRenderer.sprite = _interactIcon[(int)iconType];
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CafePlayer player) == false) return;

            if (_player == null) _player = player;
            _stateMachine.OnTriggerEnter();
        }


        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CafePlayer player) == false) return;

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
