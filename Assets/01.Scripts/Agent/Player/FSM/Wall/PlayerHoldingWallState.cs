using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldingWallState : PlayerWallState
    {
        // Wall Idle
        public PlayerHoldingWallState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            

        }

        public override void UpdateState()
        {
            base.UpdateState();
            float yInput = _player.PlayerInput.InputDirection.y;
            if (yInput > 0)
                _stateMachine.ChangeState("Climb");
            else if (yInput < 0)
                _stateMachine.ChangeState("SlideDown");
        }

    }
}