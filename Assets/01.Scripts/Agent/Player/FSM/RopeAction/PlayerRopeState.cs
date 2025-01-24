using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerRopeState : PlayerState
    {
        public PlayerRopeState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
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