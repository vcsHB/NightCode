using Agents.Animate;
using UnityEngine;

namespace Base.Cafe
{
    public class MaidNPCTalk : AvatarEntityState
    {
        private CafeMaid _maid;
        private float _talkStartTime;
        private float _talkTime = 3;

        public MaidNPCTalk(AvatarEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _maid = npc as CafeMaid;
        }

        public override void EnterState()
        {
            base.EnterState();
            _talkStartTime = Time.time;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (_talkStartTime + _talkTime < Time.time)
            {
                _maid.OnCompleteTalk();
            }
        }
    }
}
