using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.JumpEvent += HandleJump;
            _mover.SetMovementMultiplier(0.6f);
        }

        private void HandleJump()
        {
            if (_mover.CanJump)
            {
                _stateMachine.ChangeState("Jump");
            }
        }

        public override void Exit()
        {
            _player.PlayerInput.JumpEvent -= HandleJump;
            _mover.SetMovementMultiplier(1f);
            base.Exit();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            float xInput = _player.PlayerInput.InputDirection.x;
            if (Mathf.Abs(xInput) > 0)
                _mover.SetMovement(xInput);
        }

        protected override void HandleShootEvent()
        {
            if (!_player.IsActive) return;
            ShootData data = _aimController.Shoot();

            if (data.isGrabbed){
                Debug.Log("Grabbing");
                _player.StateMachine.ChangeState("Grab");
            }
            else if (data.isHanged)
                _player.StateMachine.ChangeState("Hang");
        }
    }
}