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
            float direction = _input.moveDir.x;

            if (Mathf.Abs(direction) <= 0f)
                stateMachine.ChangeState("Idle");

            if (direction != _player.MoveDir) _player.Flip();
            _player.Move();
        }
    }
}
