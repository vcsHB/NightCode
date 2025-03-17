using Agents.Animate;
using System.Collections;
using UnityEngine;

namespace Basement.NPC
{
    public class CounterState : NPCState
    {
        private CafeNPC _cafeNPC;
        private Cafe _cafe;
        private Customer _customer;
        private float _menuGetTime;

        public CounterState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _cafeNPC = npc as CafeNPC;
            _cafe = _cafeNPC.Cafe;
        }

        public override void EnterState()
        {
            base.EnterState();

            if(Mathf.Sign(_cafeNPC.MoveDir) != 1)
                _cafeNPC.Flip();
        }

        public override void UpdateState()
        {
            if (_customer == null)
            {
                if (_cafe.menuWaitingCustomers.TryPeek(out _customer))
                    _menuGetTime = Time.time;
            }
            else
            {
                if (_menuGetTime + 1 < Time.time)
                {
                    _cafeNPC.SetMoveTarget(_customer.TargetTable.servingPositionTrm);
                    _cafeNPC.SetNextState("Serving");
                    stateMachine.ChangeState("Move");
                    _customer = null;
                }
            }
        }
    }
}
