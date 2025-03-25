using Agents.Animate;
using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerJumpState : PlayerGroundState
    {
        private StatSO _jumpPower;
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;
            AgentStatus statusCompo = _player.GetCompo<AgentStatus>();
            _jumpPower = statusCompo.GetStat(StatusEnumType.JumpPower);
        }

        public override void Enter()
        {
            base.Enter();
            _mover.jumpCount --;
            Vector2 jumpPower = new Vector2(0, _jumpPower.Value);
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