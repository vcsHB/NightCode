using System.Collections;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAttackState : PlayerGroundState
    {
        protected int _comboCounter = 0;
        protected float _lastAttackTime;
        protected float _comboWindow = 0.2f;

        protected readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");


        protected Coroutine _delayCoroutine = null;


        protected Animator _animator;
        protected PlayerAttackController _attackController;

        public PlayerAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _animator = _renderer.Animator;
            _attackController = player.GetCompo<PlayerAttackController>();
        }

        public override void Enter()
        {
            base.Enter();
            bool ComboCounterOver = _comboCounter > 2;
            bool comboWindowExhaust = Time.time >= _lastAttackTime + _comboWindow;
            if (ComboCounterOver || comboWindowExhaust)
            {
                _comboCounter = 0;
            }
            _animator.SetInteger(_comboCounterHash, _comboCounter);
            MoveToAttackFacing();
        }

        protected virtual void MoveToAttackFacing()
        {
            AttackData data = _attackController.GetAttackData(_comboCounter);
            if (data == null) return;
            _mover.SetMovement(_renderer.FacingDirection * data.movePower);
            _delayCoroutine = _player.StartCoroutine(MoveDelayCoroutine(data.moveduration));

        }


        private IEnumerator MoveDelayCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            _mover.StopImmediately();
        }

        public override void Exit()
        {
            ++_comboCounter;
            _lastAttackTime = Time.time;
            _animator.speed = 1;

            if (_delayCoroutine != null)
            {
                _player.StopCoroutine(_delayCoroutine);
            }
            base.Exit();

        }


        public override void UpdateState()
        {
            base.UpdateState();

        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Idle");

        }
    }
}