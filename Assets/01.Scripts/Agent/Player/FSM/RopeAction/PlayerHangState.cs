using Agents.Animate;
using UnityEngine;

namespace Agents.Players.FSM
{
    public class PlayerHangState : PlayerRopeState
    {
        private bool _canUseTurbo = true;
        public PlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _mover.CanManualMove = false;
            _player.PlayerInput.TurboEvent += HandleUseTurbo;
            _renderer.SetLockRotation(false);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (_mover.IsGroundDetected())
            {
                _stateMachine.ChangeState("Idle");
            }
            _renderer.FlipController(_mover.Velocity.x);
            _renderer.SetRotate(_aimController.HangingDirection);
        }

        public override void Exit()
        {
            base.Exit();
            
            _player.PlayerInput.TurboEvent -= HandleUseTurbo;
            _canUseTurbo = true;
            _mover.CanManualMove = true;
            _renderer.SetLockRotation(true);

        }

        private void HandleUseTurbo()
        {
            if (!_canUseTurbo) return;
            _mover.UseTurbo(_aimController.HangingDirection);
            _canUseTurbo = false;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Turbo"));
        }

    }
}