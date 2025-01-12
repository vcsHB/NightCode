using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class KatanaPlayerState : PlayerGroundState
    {
        public KatanaPlayerState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}