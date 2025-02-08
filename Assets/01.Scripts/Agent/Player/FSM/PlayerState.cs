using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;
        protected PlayerMovement _mover;


        protected AnimParamSO _stateAnimParam;
        protected PlayerRenderer _renderer;
        protected PlayerAnimationTrigger _animationTrigger;
        protected AimController _aimController;


        protected bool _isTriggered;
        protected bool _canUseRope = false;
        protected bool _canGrab = false;
        protected bool _canRemoveRope = true;

        public PlayerState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam)
        {
            _player = player;
            _stateMachine = stateMachine;
            _stateAnimParam = animParam;
            _renderer = player.GetCompo<PlayerRenderer>();
            _mover = player.GetCompo<PlayerMovement>();
            _aimController = player.GetCompo<AimController>();

            _animationTrigger = player.GetCompo<PlayerAnimationTrigger>();
        }


        public virtual void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
            _animationTrigger.OnAnimationEnd += AnimationEndTrigger;
            if (_canUseRope)
                _player.PlayerInput.OnShootRopeEvent += HandleShootEvent;
            _player.PlayerInput.OnRemoveRopeEvent += HandleRemoveRope;

        }


        public virtual void UpdateState() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
            _animationTrigger.OnAnimationEnd -= AnimationEndTrigger;
            if (_canUseRope)
                _player.PlayerInput.OnShootRopeEvent -= HandleShootEvent;
            _player.PlayerInput.OnRemoveRopeEvent -= HandleRemoveRope;

        }


        public virtual void AnimationEndTrigger()
        {
            _isTriggered = true;
        }

        protected virtual void HandleShootEvent()
        {
            if (!_player.IsActive) return;
            ShootData data = _aimController.Shoot();

            if (data.isGrabbed && _canGrab)
            {
                //Debug.Log("Grabbing");
                _player.StateMachine.ChangeState("Grab");
            }
            else if (data.isHanged)
                _player.StateMachine.ChangeState("Hang");

        }

        protected virtual void HandleRemoveRope()
        {
            if(!_canRemoveRope) return;
            if (!_player.IsActive) return;
            _aimController.RemoveWire();
            _player.StateMachine.ChangeState("Swing");
        }

        private void HandleAttack()
        {
            if (!_player.IsActive) return;
            _stateMachine.ChangeState("Attack");
        }


    }
}