using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (_mover.IsGroundDetected())
            {
                _stateMachine.ChangeState("Idle");
            }
        }
    }
}