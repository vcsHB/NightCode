using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerRopeState : PlayerState
    {
        public PlayerRopeState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnRemoveRopeEvent += HandleRemoveRope;
        }

        public override void Exit()
        { 
            base.Exit();
            _player.PlayerInput.OnRemoveRopeEvent -= HandleRemoveRope;
        }

        protected void HandleRemoveRope()
        {
            // if(value)
            //     _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            // _aimController.HandleShootAnchor(value);

            _aimController.RemoveWire();
            _player.StateMachine.ChangeState("Swing");
        }

    }
}