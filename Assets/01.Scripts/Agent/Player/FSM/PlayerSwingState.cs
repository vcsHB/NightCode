using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerSwingState : PlayerState
    {
        private float _duration = 0.6f;
        //private Stat _playerDashPower;
        //private Stat _playerJumpPower;
        private float _currentRollingTime;
        public PlayerSwingState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            //_mover.AddForceToOuterWall(_playerDashPower.GetValue(), _playerJumpPower.GetValue());
            Vector2 velocity = _mover.Velocity;
            _mover.StopImmediately(true);
            _mover.AddForceToEntity(velocity * 0.6f);
            _currentRollingTime = 0f;
        }

        public override void UpdateState()
        {
            _currentRollingTime += Time.deltaTime;
            if (_currentRollingTime > _duration)
            {
                _stateMachine.ChangeState("Fall");
            }
            //base.UpdateState();
        }

        public override void Exit()
        {
            //_mover.StopImmediately();
            _mover.CanManualMove = true;
            base.Exit();
        }


    }
}