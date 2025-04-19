
using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class CustomerTalkState : CafeEntityState
    {
        private CafeCustomer _cafeCustomer;

        public CustomerTalkState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _cafeCustomer = npc as CafeCustomer;
        }


    }
}
