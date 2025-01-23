using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class KatanaPlayerGroundState : PlayerGroundState
    {
        public KatanaPlayerGroundState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}