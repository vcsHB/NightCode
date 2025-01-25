using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagEnterState : PlayerState
    {
        public PlayerTagEnterState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = false;
        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Idle");
        }
    }
}