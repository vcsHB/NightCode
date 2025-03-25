using UnityEngine;


namespace Cafe
{
    public class CafeCustomer : CafeEntity
    {
        public CafeCustomerSO customerSO;

        private OmeletRiceMiniGame _miniGame;
        private CafeTable _table;
        private float _foodGetTime;
        private bool _getFood = false;
        private bool _isExited = false;
        private int _rating = 3;

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

        //음식을 요청했을 때 => 앉았을 때
        public void RequireFood()
        {
            float direction = Mathf.Sign(_table.transform.position.x - transform.position.x);
            if (LookDir != direction) Flip();

            _getFood = false;
            _table.CustomerSit();
            onCompleteMove -= RequireFood;
            stateMachine.ChangeState("Sit");
        }

        public void GetFood()
        {
            if (Random.Range(0, 100) <= customerSO.minigameRequireChance)
            {
                _miniGame = CafeManager.Instance.omeletRiceMiniGame;
                _miniGame.Open();
                _miniGame.SetGuideLine(customerSO.GetRandomPainingName());
                _miniGame.onCompleteMiniGame += OnCompleteMiniGame;

                return;
            }

            OnGetFood();
        }

        //음식을 받았을 때 => 먹기 시작?
        public void OnGetFood()
        {
            _getFood = true;
            _foodGetTime = Time.time;

            if (_table.WaitingTime > customerSO.menuWaitingTime) _rating--;
            else if (_table.WaitingTime <= customerSO.menuWaitingTime / 3) _rating++;
        }

        
        public void OnCompleteMiniGame(bool isGood)
        {
            _miniGame.onCompleteMiniGame -= OnCompleteMiniGame;
            _rating += isGood ? 1 : -1;

            OnGetFood();
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
            CafeManager.Instance.AddReview(customerSO, _rating);
            onCompleteMove -= OnExitCafe;
            Destroy(gameObject);
        }


        public void Init(CafeTable table)
        {
            _table = table;
            SetMoveTarget(table.customerPosition);
            table.SetCustomer(this);
            onCompleteMove += RequireFood;
            _rating = 3;
        }
    }
}
