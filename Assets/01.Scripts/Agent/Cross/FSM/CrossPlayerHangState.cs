using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CrossPlayerHangState : PlayerHangState
    {
        public CrossPlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
        public override void UpdateState()
        {
            base.UpdateState();

        }
    }
}