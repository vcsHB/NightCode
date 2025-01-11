using UnityEngine;

namespace Agents.Players.FSM
{
    public class PlayerHangState : PlayerRopeState
    {
        private bool _canUseTurbo = true;
        public PlayerHangState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _mover.CanManualMove = false;
            _player.PlayerInput.TurboEvent += HandleUseTurbo;
            //_mover.StopImmediately(true);
        }

        public override void UpdateState()
        {
            base.UpdateState();
           
        }

        public override void Exit()
        {
            base.Exit();
            
            _player.PlayerInput.TurboEvent -= HandleUseTurbo;
            _canUseTurbo = true;

        }

        private void HandleUseTurbo()
        {
            if (!_canUseTurbo) return;
            _mover.UseTurbo();
            _canUseTurbo = false;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Turbo"));
        }

    }
}