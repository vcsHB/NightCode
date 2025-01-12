using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerAttackState : PlayerAttackState
    {
        public KatanaPlayerAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}