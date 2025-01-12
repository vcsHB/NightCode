using UnityEngine;
namespace Agents.Players.FSM
{
    public class KatanaPlayerState : PlayerGroundState
    {
        public KatanaPlayerState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }
    }
}