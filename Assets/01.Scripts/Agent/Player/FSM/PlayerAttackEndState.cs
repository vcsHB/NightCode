using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAttackEndState : PlayerIdleState
    {
        public PlayerAttackEndState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = true;
        }


        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Idle");
        }


    }
}