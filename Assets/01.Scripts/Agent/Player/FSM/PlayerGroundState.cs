using System;
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
            _aimController.RemoveWire();
            _player.PlayerInput.JumpEvent += HandleJump;
            _mover.jumpCount = 2; // 나중에 스테이터스 처리
            
        }


        public override void Exit()
        {
            _player.PlayerInput.JumpEvent -= HandleJump;
            base.Exit();

        }

        public override void UpdateState()
        {
            base.UpdateState();
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