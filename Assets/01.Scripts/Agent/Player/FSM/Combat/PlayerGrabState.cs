using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerGrabState : PlayerAirBorneState
    {
        protected bool _isComboComplete;
        public PlayerGrabState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
           
            _canUseRope = true;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            _player.PlayerInput.PullEvent += HandlePullTarget;
            _isComboComplete = false;
        }

        public override void Exit()
        {
            base.Exit();
             _player.PlayerInput.PullEvent -= HandlePullTarget;
        }

        private void HandlePullTarget()
        {
            _stateMachine.ChangeState("Pull");
        }

        protected override void HandleRemoveRope()
        {
            base.HandleRemoveRope();
            _grabThrower.ThrowTarget();
        }

        protected void SetCompleteCombo()
        {
            _isComboComplete = true;
            _grabThrower.SetCompleteCombo();
        }



    }
}