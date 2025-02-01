using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerJumpState : PlayerGroundState
    {
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.jumpCount --;
            Vector2 jumpPower = new Vector2(0, 17f);
            _mover.StopYVelocity();
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Jump"));
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