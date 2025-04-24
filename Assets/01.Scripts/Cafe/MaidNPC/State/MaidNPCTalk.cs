using Agents.Animate;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Cafe
{
    public class MaidNPCTalk : CafeEntityState
    {
        private CafeMaid _maid;
        private float _talkStartTime;
        private float _talkTime = 3;

        public MaidNPCTalk(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
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

            if(_talkStartTime + _talkTime < Time.time)
            {
                _maid.OnCompleteTalk();
            }
        }
    }
}
