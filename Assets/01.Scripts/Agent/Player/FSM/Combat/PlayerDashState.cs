using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerDashState : PlayerGroundState
    {
        public PlayerDashState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Exit()
        {
            base.Exit();

        }

    }
}