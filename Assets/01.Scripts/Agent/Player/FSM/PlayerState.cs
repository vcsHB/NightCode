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
        }


        public virtual void UpdateState() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
            _animationTrigger.OnAnimationEnd -= AnimationEndTrigger;
            if (_canUseRope)
                _player.PlayerInput.OnShootRopeEvent -= HandleShootEvent;
        }

        private void HandleShootEvent()
        {
            if (_aimController.Shoot())
                _player.StateMachine.ChangeState("Hang");
        }

        public virtual void AnimationEndTrigger()
        {
            _isTriggered = true;
        }


    }
}