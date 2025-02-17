using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAirAttackState : PlayerGrabState
    {
        protected int _comboCounter = 0;
        protected float _lastAttackTime;
        protected float _comboWindow = 0.2f;
        protected float _comboResetTime = 1f;
        protected readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
        protected Animator _animator;

        public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _animator = _renderer.Animator;
        }

        public override void Enter()
        {
            base.Enter();
            if(_lastAttackTime + _comboResetTime < Time.time)
            {
                _comboCounter = 0;
            }
            _animator.SetInteger(_comboCounterHash, _comboCounter % 2);
        }

        public override void Exit()
        {
            ++_comboCounter;
            _lastAttackTime = Time.time;
            _animator.speed = 1;
            if(_comboCounter > 5) // 임의로 설정된 최대 콤보
            {
                SetCompleteCombo();
            }

            base.Exit();

        }
        
        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Grab");

        }
    }
}