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

        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            _customer.OnSitDown();
        }
    }
}
