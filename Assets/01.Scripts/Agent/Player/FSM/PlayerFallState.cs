    using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
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