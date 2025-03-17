using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class Customer : NPC
    {
        public CustomerSO customerInfo;
        public Animator animator;
        public TalkBubble talkBubble;

        public AnimParamSO MoveParam;
        public AnimParamSO SitdownParam;
        public AnimParamSO SitParam;
        public AnimParamSO EatParam;
        public AnimParamSO StandupParam;

        private Vector2 _destination;
        private Cafe _cafe;

        public Cafe cafe => _cafe;
        public Table TargetTable { get; private set; }
        public Vector2 Destination => _destination;

        protected override void Awake()
        {
            base.Awake();

            stateMachine.AddState("Move", "Move", MoveParam);
            stateMachine.AddState("SitDown", "SitDown", SitdownParam);
            stateMachine.AddState("Sit", "Sit", SitParam);
            stateMachine.AddState("Eat", "Eat", EatParam);
            stateMachine.AddState("StandUp", "StandUp", StandupParam);
            stateMachine.ChangeState("Move");
        }

        public void OnSitDown()
        {
            _cafe.OnCustomerSitTable(this);
        }

        public void GetMenu()
        {

            stateMachine.ChangeState("Eat");
        }

        public void PayMenu()
        {

        }

        public void GiveTip()
        {

        }

        public void SetTable(Table emptyTable)
        {
            TargetTable = emptyTable;
            SetMoveTarget(emptyTable.customerPositionTrm);
        }

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
        }
    }
}
