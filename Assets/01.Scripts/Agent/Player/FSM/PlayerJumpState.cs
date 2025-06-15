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
            if (_mover.jumpCount == 1)
            {
                _renderer.SetParam(_stateAnimParam, false);
                _renderer.SetParam(_renderer.SwingParam, true);

            }
            _mover.jumpCount--;
            Vector2 jumpPower = new Vector2(0, _jumpPower.Value);
            _mover.StopYVelocity();
            _player.EventChannel.RaiseEvent(new FeedbackCreateEventData("Jump"));
            _mover.AddForceToEntity(jumpPower);
            _mover.OnMovement += HandleVelocityChnage;
        }


        public override void Exit()
        {
            _mover.OnMovement -= HandleVelocityChnage;
            _renderer.SetParam(_renderer.SwingParam, false);
            base.Exit();
        }
        public override void UpdateState()
        {
            CheckWallAndHold();
        }

        private void HandleVelocityChnage(Vector2 velocity)
        {
            if (velocity.y < -1f)
            {
                _stateMachine.ChangeState("Fall");
            }
        }
    }
}