using UnityEngine;
namespace Agents.Players.FSM
{



    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
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