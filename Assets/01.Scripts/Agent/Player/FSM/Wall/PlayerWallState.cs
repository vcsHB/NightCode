using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerWallState : PlayerState
    {
        public PlayerWallState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;

        }

        public override void Enter()
        {
            _mover.SetGravityMultiplier(0f);
            _mover.IsWallDetected();
            _renderer.FlipController(_mover.WallDirection);
            base.Enter();
            _player.PlayerInput.JumpEvent += HandleWallJump;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(!_mover.IsWallDetected())
            {
                _stateMachine.ChangeState("Fall");
            }
        }


        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.JumpEvent -= HandleWallJump;
            _mover.ResetGravityMultiplier();
        }

        private void HandleWallJump()
        {
            Vector2 jumpDirection = Vector2.one; // 벽 반대 방향 연산
            _mover.AddForceToEntity(jumpDirection);
            _stateMachine.ChangeState("Swing");
        }



    }
}