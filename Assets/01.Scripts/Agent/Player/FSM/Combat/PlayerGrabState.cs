using System;
using Agents.Animate;

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
            _grabThrower.Grab();
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
            base.HandleRemoveRope();
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