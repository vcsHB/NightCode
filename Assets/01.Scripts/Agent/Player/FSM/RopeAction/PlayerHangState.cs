using System;
using Agents.Animate;
using CameraControllers;
using UnityEngine;

namespace Agents.Players.FSM
{
    public class PlayerHangState : PlayerRopeState
    {

        protected bool _isGroundCheck = true;
        protected FeedbackCreateEventData _turboCreateFeedback = new FeedbackCreateEventData("Turbo");
        protected FeedbackFinishEventData _turboFinishFeedback = new FeedbackFinishEventData("Turbo");
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

            _mover.ResetTurboCount();
            _animationTrigger.HandleRopeShoot();
            _renderer.SetLockRotation(false);
            _aimController.SetOrbitVisual(true);
            CameraManager.Instance.GetCompo<CameraZoomController>().SetZoomLevel(30, 1f, true);
        }


        public override void UpdateState()
        {
            base.UpdateState();

            _renderer.FlipController(_mover.Velocity.normalized.x);
            _renderer.SetRotate(_aimController.HangingDirection);
            _aimController.RefreshOrbitVisual();


            CheckExitToGround();

            if (_mover.Velocity.magnitude < 0.6f)
            {
                if (CheckWallAndHold())
                {
                    //HandleRemoveRope();
                    _aimController.RemoveWire();
                }

            }
        }

        protected void CheckExitToGround()
        {
            if (_isGroundCheck)
            {

                if (_mover.IsGroundDetected() && _aimController.HangingDirection.y < 0)
                {
                    _aimController.RemoveWire();
                    _stateMachine.ChangeState("Fall");
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            _player.PlayerInput.TurboEvent -= HandleUseTurbo;
            _player.PlayerInput.PullEvent -= HandlePull;

            _mover.CanManualMove = true;
            _aimController.SetOrbitVisual(false);
            _player.EventChannel.RaiseEvent(_turboFinishFeedback);


            _renderer.SetLockRotation(true);

        }

        protected virtual void HandleUseTurbo()
        {
            if (!_player.IsActive) return;
            if (!_mover.CanUseTurbo) return;

            ForceUseTurbo();

        }

        protected virtual void ForceUseTurbo()
        {
            _aimController.RefreshHangingDirection();
            _animationTrigger.HandleTurboEvent();
            _mover.UseTurbo(_aimController.HangingDirection);
            
            _player.EventChannel.RaiseEvent(_turboCreateFeedback);
        }

        protected void HandlePull()
        {
            if (!_player.IsActive) return;
            _isGroundCheck = false;
            _animationTrigger.HandleGroundPullStart();
            _player.EventChannel.RaiseEvent(_turboFinishFeedback);
            _aimController.HandlePull(HandleArriveAttack);
        }

        protected void HandleArriveAttack()
        {
            Vector2 bounceDirection = -_aimController.HangingDirection.normalized;
            bounceDirection.y += 1;
            _animationTrigger.HandleGroundPullArrive();
            HandleRemoveRope();
            _isGroundCheck = true;
            _mover.SetVelocity(bounceDirection * 20f);
        }


    }
}