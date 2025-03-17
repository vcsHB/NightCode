using System;
using Agents.Animate;
using CameraControllers;
using UnityEngine;

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
            _isGroundCheck = true;
            _player.PlayerInput.TurboEvent += HandleUseTurbo;
            _player.PlayerInput.PullEvent += HandlePull;
            _renderer.SetLockRotation(false);
            CameraManager.Instance.GetCompo<CameraZoomController>().SetZoomLevel(30, 1f, true);
        }


        public override void UpdateState()
        {
            base.UpdateState();

            _renderer.FlipController(_mover.Velocity.normalized.x);
            _renderer.SetRotate(_aimController.HangingDirection);


            if (_mover.Velocity.magnitude < 0.3f)
            {
                if (CheckWallAndHold())
                {
                    //HandleRemoveRope();
                    _aimController.RemoveWire();
                }
                if (_isGroundCheck)
                {

                    if (_mover.IsGroundDetected())
                    {
                        _stateMachine.ChangeState("Idle");
                    }
                }
            }
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

            _aimController.RefreshHangingDirection();
            _mover.UseTurbo(_aimController.HangingDirection);
            _canUseTurbo = false;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Turbo"));
        }

        private void HandlePull()
        {
            if (!_player.IsActive) return;
            _isGroundCheck = false;
            _aimController.HandlePull(HandleArriveAttack);
        }

        private void HandleArriveAttack()
        {
            Vector2 bounceDirection = -_aimController.HangingDirection.normalized;
            bounceDirection.y += 1;
            _animationTrigger.HandleGroundPullAttack();
            HandleRemoveRope();
            _isGroundCheck = true;
            _mover.SetVelocity(bounceDirection * 20f);
        }
    }
}