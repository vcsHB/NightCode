using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagExitState : PlayerState
    {
        public PlayerTagExitState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}