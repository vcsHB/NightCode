using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerGrabState : PlayerState
    {
        protected GrabThrower _grabThrower;
        protected PlayerAttackController _attackController;

        protected bool _isComboComplete;
        public PlayerGrabState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _grabThrower = player.GetCompo<GrabThrower>();
            _attackController = player.GetCompo<PlayerAttackController>();
            _canUseRope = true;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);

            _isComboComplete = false;
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