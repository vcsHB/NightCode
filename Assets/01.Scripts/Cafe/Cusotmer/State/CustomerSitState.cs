using Agents.Animate;
using UnityEngine;

namespace Base.Cafe
{
    public class CustomerSitState : BaseEntityState
    {
        private CafeCustomer _customer;

        public CustomerSitState(BaseEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
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
