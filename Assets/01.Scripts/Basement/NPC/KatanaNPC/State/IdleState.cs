using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class IdleState : NPCState
    {
        private float _idleEnterTime;
        private float _idleTime;

        private bool _playAnim;

        public IdleState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            _idleEnterTime = Time.time;
            _idleTime = npc.npcSO.GetRandomIdleTime();
            npc.StopImmediatly();

            AnimParamSO animParam = npc.npcSO.GetRandomEmotion();
            if(animParam != null )
            {
                npc.curAnimParam = animParam.hashValue;
                stateMachine.ChangeState("Emotion");
            }
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if(_idleEnterTime + _idleTime <= Time.time)
                stateMachine.ChangeState("Roaming");
        }
    }
}
