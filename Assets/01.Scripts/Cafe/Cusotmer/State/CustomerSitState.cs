using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class CustomerSitState : CafeEntityState
    {
        private CafeCustomer customer;

        public CustomerSitState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            customer = npc as CafeCustomer;
        }


    }
}
