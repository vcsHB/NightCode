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
            _mover.SetYMovement(0f);
            _mover.IsWallDetected();
            _renderer.FlipController(_mover.WallDirection);
            base.Enter();
            _player.PlayerInput.JumpEvent += HandleWallJump;
        }

        public override void UpdateState()
        {
            if (!_mover.IsWallDetected())
            {
                _mover.SetYMovement(0f);
                _mover.ResetGravityMultiplier();
                _mover.StopImmediately(true);
                //_stateMachine.ChangeState("Fall");
                HandleWallJump();
            }
        }


        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.JumpEvent -= HandleWallJump;
            //_mover.StopImmediately(true);
            _mover.SetYMovement(0f);
            _mover.ResetGravityMultiplier();
        }

        private void HandleWallJump()
        {
            Vector2 jumpDirection = new Vector2(-_mover.WallDirection * 20f, 10f); // 벽 반대 방향 연산
            _mover.CanManualMove = false;
            _mover.SetVelocity(jumpDirection);
            
            _stateMachine.ChangeState("Swing");
        }



    }
}