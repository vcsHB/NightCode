using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{



    public class PlayerMoveState : PlayerAttackableState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            float xInput = _player.PlayerInput.InputDirection.x;
            _mover.SetMovement(xInput);
            Debug.Log("Move Update");
            if(Mathf.Approximately(xInput, 0))
            {
                _stateMachine.ChangeState("Idle");
            }
        }
    }
}