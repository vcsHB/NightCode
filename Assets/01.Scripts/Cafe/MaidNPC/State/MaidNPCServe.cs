using Agents.Animate;
using UnityEngine;

namespace Base.Cafe
{
    public class MaidNPCServe : AvatarEntityState
    {
        private CafeMaid _maid;
        public MaidNPCServe(AvatarEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
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
