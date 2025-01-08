using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerJumpState : PlayerGroundState
    {
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Vector2 jumpPower = new Vector2(0, 13f);
            //_mover.StopImmediately(true);
            _mover.AddForceToEntity(jumpPower);
            _mover.OnMovement += HandleVelocityChnage;
        }


        public override void Exit()
        {
            _mover.OnMovement -= HandleVelocityChnage;
            base.Exit();
        }

        private void HandleVelocityChnage(Vector2 velocity)
        {
            if (velocity.y < 0)
            {
                _stateMachine.ChangeState("Fall");
            }
        }
    }
}