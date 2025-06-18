using Base.Cafe;
using UnityEngine;
using Agents.Animate;

namespace Base
{
    public class PlayerMoveState : AvatarEntityState
    {

        private AvatarPlayer _player;
        private BaseInput _input;

        public PlayerMoveState(AvatarEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as AvatarPlayer;
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
