using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class MoveState : NPCState
    {
        private NPC _npc;

        public MoveState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _npc = npc;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            float distance = Vector2.Distance(_npc.MoveTarget.position, _npc.transform.position);
            
            float direction = Mathf.Sign(_npc.MoveTarget.position.x - _npc.transform.position.x);
            if (Mathf.Sign(direction) != Mathf.Sign(_npc.MoveDir)) _npc.Flip();

            if (distance < 0.4f)
            {
                stateMachine.ChangeState(_npc.NextState);
            }
            else
                _npc.Move(_npc.MoveDir);
        }
    }
}
