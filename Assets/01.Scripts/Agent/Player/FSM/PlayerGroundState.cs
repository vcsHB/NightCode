using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerGroundState : PlayerState
    {

        public PlayerGroundState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _renderer.SetLockRotation(true);
            if(_aimController.IsShoot)
                _aimController.RemoveWire();
            _player.PlayerInput.JumpEvent += HandleJump;
            _player.PlayerInput.TurboEvent += HandleDash;
            
        }


        public override void Exit()
        {
            _player.PlayerInput.JumpEvent -= HandleJump;
            _player.PlayerInput.TurboEvent -= HandleDash;

            base.Exit();

        }

        private void HandleDash()
        {
            // 대쉬 구현
        }

        public override void UpdateState()
        {
            base.UpdateState();
            CheckWallAndHold();
            if (_mover.IsGroundDetected() == false && _mover.CanManualMove)
            {
                _stateMachine.ChangeState("Fall");
            }
        }


        private void HandleJump()
        {
            if(_mover.CanJump)
                _stateMachine.ChangeState("Jump");
        }
    }
}