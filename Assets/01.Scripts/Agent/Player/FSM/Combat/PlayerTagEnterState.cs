using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagEnterState : PlayerState
    {
        public PlayerTagEnterState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}