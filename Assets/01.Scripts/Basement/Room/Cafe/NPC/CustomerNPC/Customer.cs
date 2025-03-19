using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class Customer : NPC
    {
        public CustomerSO customerInfo;
        public TalkBubble talkBubble;

        private Cafe _cafe;

        #region Property

        public Cafe cafe => _cafe;
        public Table TargetTable { get; private set; }
        public FoodSO RequireFood { get; private set; }
        public float FoodEatingTime { get; private set; }

        #endregion

        protected void Start()
        {
            stateMachine.AddState("Move", "Move", customerInfo.MoveParam);
            stateMachine.AddState("SitDown", "SitDown", customerInfo.SitdownParam);
            stateMachine.AddState("Sit", "Sit", customerInfo.SitParam);
            stateMachine.AddState("Eat", "Eat", customerInfo.EatParam);
            stateMachine.AddState("StandUp", "StandUp", customerInfo.StandupParam);
            stateMachine.ChangeState("Move");
        }

        private void OnExit()
        {
            _cafe.OnLeaveCustomer(this);

            onCompleteMove -= OnExit;
            Destroy(gameObject);
        }

        public void OnSitDown()
        {
            RequireFood = customerInfo.GetRandomFood();
            talkBubble.SetIcon(RequireFood.icon);
            talkBubble.Open();

            _cafe.OnCustomerSitTable(this);
            stateMachine.ChangeState("Sit");
        }

        public void GetMenu()
        {
            GiveTip();
            talkBubble.Close();
            FoodEatingTime = customerInfo.GetRandomEatingTime();
            stateMachine.ChangeState("Eat");
        }

        public void PayCost()
        {
            Vector2 popupPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1));
            UIManager.Instance.SetPopupText($"<color=green>{RequireFood.cost}$</color>", popupPosition);

            NextState = "Move";
            onCompleteMove += OnExit;
            SetMoveTarget(_cafe.exit);
            stateMachine.ChangeState("Move");
            TargetTable.CustomerLeave();
        }

        public void GiveTip()
        {
            bool willGiveTip = customerInfo.CheckGiveTip();
            int tip = Mathf.RoundToInt(RequireFood.cost * 0.3f);

            Vector2 popupPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1));
            UIManager.Instance.SetPopupText($"<color=green>TIP {tip}$</color>", popupPosition);
        }

        public void SetTable(Table emptyTable)
        {
            TargetTable = emptyTable;
            SetMoveTarget(emptyTable.customerPositionTrm);
            TargetTable.CustomerSitdown(this);              //데이터 상으로는 미리 할당해줘야함
            NextState = "SitDown";
        }

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
        }
    }
}
