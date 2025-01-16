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

        protected bool _isTriggered;

        public PlayerState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam)
        {
            _player = player;
            _stateMachine = stateMachine;
            _stateAnimParam = animParam;
            _renderer = player.GetCompo<PlayerRenderer>();
            _mover = player.GetCompo<PlayerMovement>();
            _animationTrigger = player.GetCompo<PlayerAnimationTrigger>();
        }


        public virtual void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
            _animationTrigger.OnAnimationEnd += AnimationEndTrigger;
        }
        public virtual void UpdateState() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
            _animationTrigger.OnAnimationEnd -= AnimationEndTrigger;
        }



        public virtual void AnimationEndTrigger()
        {
            Debug.Log("Animation End Trigger");
            _isTriggered = true;
        }
    }
}