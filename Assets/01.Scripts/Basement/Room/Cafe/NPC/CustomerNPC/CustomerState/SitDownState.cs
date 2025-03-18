using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class SitDownState : NPCState
    {
        private Customer _customer;
        
        public SitDownState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _customer = npc as Customer;
        }

        //얘도 디버깅용 애니메이션 넣으면 바꾸면 됨
        public override void EnterState()
        {
            base.EnterState();
            _customer.OnSitDown();
        }

        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            _customer.OnSitDown();
        }
    }
}
