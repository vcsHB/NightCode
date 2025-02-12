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
            base.UpdateState();
            float yInput = _player.PlayerInput.InputDirection.y;
            _mover.SetYMovement(yInput * 7f);
            if(yInput <= 0f)
            {
                _stateMachine.ChangeState("HoldingWall");
            }
        }
    }
}