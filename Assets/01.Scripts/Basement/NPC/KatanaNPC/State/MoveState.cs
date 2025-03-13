using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class MoveState : NPCState
    {
        private CafeNPC _cafeNPC;

        public MoveState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _cafeNPC = npc as CafeNPC;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            float distance = Vector2.Distance(_cafeNPC.MoveTarget.position, _cafeNPC.transform.position);
            
            float direction = Mathf.Sign(_cafeNPC.MoveTarget.position.x - _cafeNPC.transform.position.x);
            if (Mathf.Sign(direction) != Mathf.Sign(_cafeNPC.MoveDir)) _cafeNPC.Flip();

            if (distance < 0.4f)
            {
                stateMachine.ChangeState(_cafeNPC.NextState);
            }
            else
                _cafeNPC.Move(_cafeNPC.MoveDir);
        }
    }
}
