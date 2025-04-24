using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class MaidNPCServe : CafeEntityState
    {
        private CafeMaid _maid;
        public MaidNPCServe(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _maid = npc as CafeMaid;
        }

        public override void EnterState()
        {
            base.EnterState();

            _maid.ServeMenu();
        }
    }
}
