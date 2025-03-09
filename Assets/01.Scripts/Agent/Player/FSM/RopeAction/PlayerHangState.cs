using Agents.Animate;
using CameraControllers;

namespace Agents.Players.FSM
{
    public class PlayerHangState : PlayerRopeState
    {
        private bool _canUseTurbo = true;

        private bool _isGroundCheck = true;
        public PlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            _player.PlayerInput.TurboEvent += HandleUseTurbo;
            _player.PlayerInput.PullEvent += HandlePull;
            _renderer.SetLockRotation(false);
            CameraManager.Instance.GetCompo<CameraZoomController>().SetZoomLevel(30, 1f, true);
        }


        public override void UpdateState()
        {
            base.UpdateState();
            // if (_mover.IsGroundDetected())
            // {
            //     _stateMachine.ChangeState("Idle");
            // }
            _renderer.FlipController(_mover.Velocity.normalized.x);
            _renderer.SetRotate(_aimController.HangingDirection);
        }

        public override void Exit()
        {
            base.Exit();

            _player.PlayerInput.TurboEvent -= HandleUseTurbo;
            _player.PlayerInput.PullEvent -= HandlePull;

            _canUseTurbo = true;
            _mover.CanManualMove = true;
            _renderer.SetLockRotation(true);

        }

        private void HandleUseTurbo()
        {
            if (!_player.IsActive) return;
            if (!_canUseTurbo) return;
            _mover.UseTurbo(_aimController.HangingDirection);
            _canUseTurbo = false;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Turbo"));
        }

        private void HandlePull()
        {
            if (!_player.IsActive) return;
            _aimController.HandlePull();
        }
    }
}