using System;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerGroundState : PlayerState
    {

        public PlayerGroundState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {

        }

        public override void Enter()
        {
            base.Enter();

            _player.PlayerInput.JumpEvent += HandleJump;
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

            _stateMachine.ChangeState("Jump");
        }
    }
}