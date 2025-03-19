using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerClimbState : PlayerWallState
    {
        public PlayerClimbState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void UpdateState()
        {
            float yInput = _player.PlayerInput.InputDirection.y;
            _mover.SetYMovement(yInput * 20f);
            if (yInput <= 0f)
            {
                _stateMachine.ChangeState("HoldingWall");
            }
            if (!_mover.IsWallDetected())
            {
                //_mover.SetYMovement(5f);
                _mover.ResetGravityMultiplier();
                _stateMachine.ChangeState("Fall");
                //HandleWallJump();
            }
        }
    }
}