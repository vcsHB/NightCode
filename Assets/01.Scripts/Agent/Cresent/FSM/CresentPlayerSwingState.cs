using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    [Obsolete("Discarded State Class")]
    public class CresentPlayerSwingState : PlayerSwingState
    {
        private CresentPlayerAnimationTrigger _cresentPlayerAnimationTrigger;
        private CresentPlayer _cresentPlayer;
        private StaminaController _staminaController;
        public CresentPlayerSwingState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _cresentPlayer = player as CresentPlayer;
            _staminaController = player.GetCompo<StaminaController>();
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
            if (velocity.magnitude > _cresentPlayer.DashAttackStandardVelocity
                && _staminaController.CheckEnough(1))
            {
                _mover.SetGravityMultiplier(0f);
                _staminaController.ReduceStamina();
                //Vector2 newDirection = VectorCalculator.ClampTo8Directions(_mover.Velocity) * velocity.magnitude;
                _mover.SetVelocity(_aimController.AimDirection.normalized * 120f);
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("SwingDash"));
            }
            else
            {
                _mover.StopImmediately(true);
                _mover.AddForceToEntity(velocity);

            }

        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void Exit()
        {
            base.Exit();
            _mover.ResetGravityMultiplier();
            //Time.timeScale = 1f;
            _mover.SetMovementMultiplier(1f);
        }
    }

}
