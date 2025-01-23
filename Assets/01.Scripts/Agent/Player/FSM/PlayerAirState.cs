using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetMovementMultiplier(0.85f);
        }

        public override void Exit()
        {
            _mover.SetMovementMultiplier(1f);
            base.Exit();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            float xInput = _player.PlayerInput.InputDirection.x;
            if (Mathf.Abs(xInput) > 0)
                _mover.SetMovement(xInput);
        }
    }
}