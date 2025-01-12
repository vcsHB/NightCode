using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerAttack1State : KatanaPlayerState
    {
        public KatanaPlayerAttack1State(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }
    }
}