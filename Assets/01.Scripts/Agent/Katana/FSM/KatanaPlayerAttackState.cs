using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerAttack1State : KatanaPlayerState
    {
        public KatanaPlayerAttack1State(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}