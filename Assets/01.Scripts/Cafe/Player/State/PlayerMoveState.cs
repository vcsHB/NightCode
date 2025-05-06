using Base.Cafe;
using UnityEngine;
using Agents.Animate;

namespace Base
{
    public class PlayerMoveState : BaseEntityState
    {

        private BasePlayer _player;
        private BaseInput _input;

        public PlayerMoveState(BaseEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as BasePlayer;
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
