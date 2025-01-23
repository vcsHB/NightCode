using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Fall Update");
            if (_mover.IsGroundDetected())
            {
                _stateMachine.ChangeState("Idle");
            }
        }
    }
}