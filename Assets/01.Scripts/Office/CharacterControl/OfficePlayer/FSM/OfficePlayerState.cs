using System;
using Agents.Animate;
using UnityEngine;
namespace Office.CharacterControl
{

    public class OfficePlayerState
    {
        protected OfficePlayer _owner;
        protected OfficePlayerStateMachine _stateMachine;
        protected OfficePlayerMovement _mover;


        protected AnimParamSO _stateAnimParam;
        protected OfficePlayerRenderer _renderer;
        protected OfficePlayerTrigger _animationTrigger;

        protected bool _isTriggered;


        public OfficePlayerState(OfficePlayer player, OfficePlayerStateMachine stateMachine, AnimParamSO paramSO)
        {
            _owner = player;
            _stateMachine = stateMachine;
            _mover = player.GetCompo<OfficePlayerMovement>();
            _renderer = player.GetCompo<OfficePlayerRenderer>();
            _animationTrigger = player.GetCompo<OfficePlayerTrigger>();

        }

        public virtual void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
            _animationTrigger.OnAnimationEnd += AnimationEndTrigger;
        }


        public virtual void Update() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
            _animationTrigger.OnAnimationEnd -= AnimationEndTrigger;
        }

        public virtual void AnimationEndTrigger()
        {
            _isTriggered = true;

        }



    }
}