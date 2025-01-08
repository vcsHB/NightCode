using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerRopeState : PlayerState
    {
        protected AimController _aimController;
        public PlayerRopeState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
            _aimController = player.GetCompo<AimController>();
        }
        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.ShootEvent += HandleShoot;
        }
        public override void Exit()
        { 
            base.Exit();
            _player.PlayerInput.ShootEvent -= HandleShoot;
        }

        protected void HandleShoot(bool value)
        {
            _aimController.HandleShootAnchor(value);
        }
    }
}