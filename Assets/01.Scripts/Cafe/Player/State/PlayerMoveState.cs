using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class PlayerMoveState : CafeEntityState
    {

        private CafePlayer _player;
        private CafeInput _input;

        public PlayerMoveState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as CafePlayer;
            _input = _player.input;
        }

        public override void UpdateState()
        {
            float direction = _input.MoveDir.x;

            if (Mathf.Abs(direction) <= 0.2f)
            {
                stateMachine.ChangeState("Idle");
                return;
            }

            _player.Move(direction);
        }
    }
}
