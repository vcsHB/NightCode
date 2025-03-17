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

        protected override void Awake()
        {
            base.Awake();

            stateMachine.AddState("Move", "Move", customerInfo.MoveParam);
            stateMachine.AddState("SitDown", "SitDown", customerInfo.SitdownParam);
            stateMachine.AddState("Sit", "Sit", customerInfo.SitParam);
            stateMachine.AddState("Eat", "Eat", customerInfo.EatParam);
            stateMachine.AddState("StandUp", "StandUp", customerInfo.StandupParam);
            stateMachine.ChangeState("Move");
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

            SetMoveTarget(_cafe.exit);
            stateMachine.ChangeState("Move");
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
            NextState = "SitDown";
        }

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
        }
    }
}
