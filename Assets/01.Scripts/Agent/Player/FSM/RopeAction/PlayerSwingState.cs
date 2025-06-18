using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerSwingState : PlayerState
    {
        protected float _duration = 0.8f;
        //private Stat _playerDashPower;
        //private Stat _playerJumpPower;
        protected float _currentRollingTime;
        protected float _floatingDuration = 0.2f;
        protected float _stateEnterTime;

        public PlayerSwingState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;

            _canGrab = true;
        }


        public override void Enter()
        {
            base.Enter();

            _mover.SetMovementMultiplier(1f);
            _mover.CanManualMove = false;
            Vector2 velocity = _mover.Velocity;
            _mover.StopImmediately(true);
            // velocity.x *= 0.8f;
            // velocity.y *= 0.6f;
            _mover.AddForceToEntity(velocity);
            _stateEnterTime = Time.time;
            _currentRollingTime = 0f;
        }

        public override void UpdateState()
        {
            _currentRollingTime += Time.deltaTime;

            if (_stateEnterTime + _floatingDuration > Time.time) return;
            if (_currentRollingTime > _duration)
            {
                _stateMachine.ChangeState("Fall");
            }
            if (_mover.IsGroundDetected() && !_aimController.IsShoot)
            {
                _stateMachine.ChangeState("Idle");
                return;
            }
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            base.Exit();
        }


    }
}