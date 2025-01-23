using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAttackState : PlayerGroundState
    {
        protected int _comboCounter = 0;
        protected float _lastAttackTime;
        protected float _comboWindow = 0.4f;

        protected readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");


        protected Coroutine _delayCoroutine = null;


        protected Animator _animator;

        public PlayerAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            Debug.Log(player);
            _animator = player.GetComponentInChildren<Animator>();
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
    }
}