using Dialog;
using System;
using UnityEngine;
using UnityEngine.Events;


namespace Base.Cafe
{
    public class CafeCustomer : AvatarEntity
    {
        public CafeCustomerSO customerSO;
        public event Action onExitCafe;
        public UnityEvent unitCompleteTalk;

        private InGameDialogPlayer _dialogPlayer;
        private OmeletRiceMiniGame _miniGame;
        private CafeSit _table;
        private float _foodGetTime;
        private bool _getFood = false;
        private bool _isExited = false;

        protected override void Awake()
        {
            base.Awake();
            _dialogPlayer = FindAnyObjectByType<InGameDialogPlayer>();
        }

        protected override void Update()
        {
            base.Update();
            if (_getFood && _foodGetTime + 2f < Time.time)
            {
                MoveTarget = transform.parent;

                OnLeaveTable();
                _table.LeaveCustomer();
            }
        }

        //������ ��û���� �� => �ɾ��� ��
        public void RequireFood()
        {
            float direction = Mathf.Sign(_table.transform.position.x - transform.position.x);
            onCompleteMove -= RequireFood;
            if (LookDir != direction) Flip();

            _getFood = false;
            _table.CustomerSit();
            stateMachine.ChangeState("Sit");
        }

        //TODO Set Delay When Call This Funtion
        public void GetFood()
        {
            _miniGame = CafeManager.Instance.omeletRiceMiniGame;
            _miniGame.SetGuideLine(customerSO.GetRandomPainingName());
            _miniGame.onCompleteMiniGame += OnCompleteMiniGame;
            _miniGame.Open();
        }



        public void OnCompleteMiniGame(bool isGood)
        {
            if (_miniGame != null)
            {
                _miniGame.onCompleteMiniGame -= OnCompleteMiniGame;
            }

            if (customerSO.isInteractiveCustomer)
            {
                CafeManager.Instance.input.DisableInput();
                _dialogPlayer.OnDialogEnd += OnGetFood;
                _dialogPlayer.SetDialog(customerSO.talk);
                _dialogPlayer.StartDialog();
            }
            else
            {
                OnGetFood();
            }
        }

        //������ �޾��� �� => �Ա� ����?
        public void OnGetFood()
        {
            _getFood = true;
            CafeManager.Instance.input.EnableInput();
            _foodGetTime = Time.time;
            unitCompleteTalk?.Invoke();
            _dialogPlayer.OnDialogEnd -= OnGetFood;
        }



        public void OnLeaveTable()
        {
            onCompleteMove += OnExitCafe;
            stateMachine.ChangeState("Move");
        }

        public void OnExitCafe()
        {
            if (_isExited) return;

            _isExited = true;
            onExitCafe?.Invoke();
            onCompleteMove -= OnExitCafe;
            Destroy(gameObject);
        }


        public void Init(CafeSit table, CafeCustomerSO customerSO)
        {
            _table = table;
            this.customerSO = customerSO;
            SetMoveTarget(table.customerPosition);
            _table.SetCustomer(this);
            onCompleteMove += RequireFood;
        }
    }
}
