using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CrossPlayerSwingState : PlayerSwingState
    {
        private CrossPlayerAnimationTrigger _crossAnimTrigger;
        public CrossPlayerSwingState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _crossAnimTrigger = _player.GetCompo<CrossPlayerAnimationTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _crossAnimTrigger.SwingAttack();
        }


    }
}