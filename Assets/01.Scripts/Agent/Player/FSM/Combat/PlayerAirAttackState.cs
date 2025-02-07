using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAirAttackState : PlayerGrabState
    {

        protected readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
        protected Animator _animator;

        public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }
    }
}