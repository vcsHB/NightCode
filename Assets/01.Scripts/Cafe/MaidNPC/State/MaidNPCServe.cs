using Agents.Animate;
using UnityEngine;

namespace Base.Cafe
{
    public class MaidNPCServe : BaseEntityState
    {
        private CafeMaid _maid;
        public MaidNPCServe(BaseEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
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
