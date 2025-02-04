using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAirAttackState : PlayerGrabState
    {
        public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}