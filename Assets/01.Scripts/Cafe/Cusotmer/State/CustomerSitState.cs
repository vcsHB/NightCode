using Agents.Animate;
using UnityEngine;

namespace Base.Cafe
{
    public class CustomerSitState : AvatarEntityState
    {
        private CafeCustomer _customer;

        public CustomerSitState(AvatarEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
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
