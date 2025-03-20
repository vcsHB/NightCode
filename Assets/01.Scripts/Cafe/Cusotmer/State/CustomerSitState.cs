using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class CustomerSitState : CafeEntityState
    {
        private CafeCustomer _customer;

        public CustomerSitState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _customer = npc as CafeCustomer;
        }

        public override void EnterState()
        {
            base.EnterState();
            _customer.StopImmediatly();
        }

    }
}
