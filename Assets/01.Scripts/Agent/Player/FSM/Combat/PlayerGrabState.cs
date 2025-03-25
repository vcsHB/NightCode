using System;
using Agents.Animate;

namespace Agents.Players.FSM
{

    public class PlayerGrabState : PlayerAirBorneState
    {
        protected bool _isComboComplete;
        private bool _isGrabRelease;
        public PlayerGrabState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {

            _canUseRope = true;
        }

        public override void Enter()
        {
            _mover.StopImmediately(true);
            if (_player.PlayerInput.IsShootRelease) HandleRemoveRope();
            _grabThrower.Grab();
            base.Enter();
            _player.PlayerInput.PullEvent += HandlePullTarget;
            _player.PlayerInput.OnAttackEvent += HandleAttack;
            _renderer.FlipController(_aimController.AimDirection.x);
            _isComboComplete = false;
        }


        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.PullEvent -= HandlePullTarget;
            _player.PlayerInput.OnAttackEvent -= HandleAttack;
        }

        private void HandlePullTarget()
        {
            if (!_grabThrower.IsPulled)
                _stateMachine.ChangeState("Pull");
            //SetCompleteCombo(); // 나중에 빼기
        }

        protected override void HandleRemoveRope()
        {
            _isGrabRelease = false;
            if (!_canRemoveRope) return;
            if (!_player.IsActive) return;
            _aimController.RemoveWire();
            if (_grabThrower.IsPulled)
                _grabThrower.ThrowTarget();
        }

        protected void SetCompleteCombo()
        {
            _isComboComplete = true;
            _grabThrower.SetCompleteCombo();
        }


        private void HandleAttack()
        {
            _stateMachine.ChangeState("AirAttack");
        }

    }
}