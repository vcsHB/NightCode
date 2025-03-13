using Agents.Animate;
using UnityEngine;
using UnityEngine.UIElements;

namespace Basement.NPC
{
    public class ServingState : NPCState
    {
        private CafeNPC _cafeNPC;
        private Cafe _cafe;
        private Customer _customer;
        private float _enterTime;

        public ServingState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _cafeNPC = npc as CafeNPC;
            _cafe = _cafeNPC.Cafe;
        }

        public override void EnterState()
        {
            base.EnterState();

            _enterTime = Time.time;
            _customer = _cafe.menuWaitingCustomers.Dequeue();
            
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (_enterTime + 1f < Time.time)
            {
                _cafeNPC.talkBubble.Open();
                _cafeNPC.talkBubble.SetIcon(_cafeNPC.heartIcon);
            }

            if (_enterTime + 4f < Time.time)
            {
                _cafeNPC.talkBubble.Close();
                _cafeNPC.SetNextState("Counter");
                _cafeNPC.SetMoveTarget(_cafe.employeePosition);
                stateMachine.ChangeState("Move");
                _customer.ServeMenu();
            }
        }


        //이건 나중에 애니메이션 추가하고 나서하는걸루
        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            _cafeNPC.SetMoveTarget(_cafe.employeePosition);
            _cafeNPC.SetMoveTarget(_cafe.employeePosition);
            stateMachine.ChangeState("Move");
        }
    }
}
