using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagExitState : PlayerState
    {
        public PlayerTagExitState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            HandleRemoveRope();
            _renderer.SetDissolve(false, () => 
            {
                _player.gameObject.SetActive(false);
            });

        }
    }
}