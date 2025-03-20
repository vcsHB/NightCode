using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CresentPlayerSwingState : PlayerSwingState
    {
        private CresentPlayerAnimationTrigger _cresentPlayerAnimationTrigger;
        private float _standardVelocity = 50f;
        public CresentPlayerSwingState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _cresentPlayerAnimationTrigger = player.GetCompo<CresentPlayerAnimationTrigger>();
            _duration = 0.2f;
        }

        public override void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
            _animationTrigger.OnAnimationEnd += AnimationEndTrigger;
            if (_canUseRope)
                _player.PlayerInput.OnShootRopeEvent += HandleShootEvent;
            _player.PlayerInput.OnRemoveRopeEvent += HandleRemoveRope;
            // 

            _mover.CanManualMove = false;
            _currentRollingTime = 0f;
            _stateEnterTime = Time.time;
            Vector2 velocity = _mover.Velocity;
            if (velocity.magnitude > _standardVelocity)
            {
                _mover.SetVelocity(velocity * 15f);
            }

        }

        public override void UpdateState()
        {
            _cresentPlayerAnimationTrigger.CastSwingAttackCaster();
            base.UpdateState();
        }

        public override void Exit()
        {
            base.Exit();
            //Time.timeScale = 1f;

            _mover.SetMovementMultiplier(1f);
        }
    }

}
