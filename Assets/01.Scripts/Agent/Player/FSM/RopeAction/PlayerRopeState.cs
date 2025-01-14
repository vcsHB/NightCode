using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerRopeState : PlayerState
    {
        protected AimController _aimController;
        public PlayerRopeState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _aimController = player.GetCompo<AimController>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.LeftClickEvent += HandleShoot;
        }

        public override void Exit()
        { 
            base.Exit();
            _player.PlayerInput.LeftClickEvent -= HandleShoot;
        }

        protected void HandleShoot(bool value)
        {
            // if(value)
            //     _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            // _aimController.HandleShootAnchor(value);
        }
        
    }
}