using System;
using UnityEngine;
using UnityEngine.Events;
namespace Combat
{

    public class StunBody : MonoBehaviour, IStunable
    {
        public UnityEvent OnStunEvent;
        public UnityEvent OnStunCompleteEvent;
        public event Action<float, float> OnStunLevelChangedEvent;
        [SerializeField] private float _currentStunLevel;
        [SerializeField] private float _maxStunLevel = 100f;
        [SerializeField] private float _stunReduceDelay;
        [SerializeField] private float _stunReduceMultiplier = 2f;
        [SerializeField] private float _stunCompleteRecoverCooltime = 10f; // 완전기절 후 회복까지 쿨타임
        private float _currentStunReduceCoolTime = 0f;

        private void Awake()
        {

        }

        private void Update()
        {
            _currentStunReduceCoolTime += Time.deltaTime;
            if (_currentStunReduceCoolTime > _stunReduceDelay)
            {
                _currentStunLevel -= Time.deltaTime * _stunReduceMultiplier;
                InvokeStunEvent();
            }
        }

        /// <summary>
        ///     Stun Target
        /// </summary>
        /// <param name="stun"></param>
        /// <returns>Is Stun Completely</returns>
        public bool Stun(float stun)
        {
            _currentStunLevel += stun;
            _currentStunReduceCoolTime = 0f;
            InvokeStunEvent();
            if (_currentStunLevel >= _maxStunLevel)
            {
                StunCompletely();
                return true;
            }
            return false;
        }

        private void StunCompletely()
        {
            OnStunCompleteEvent?.Invoke();
        }

        private void InvokeStunEvent()
        {
            OnStunLevelChangedEvent?.Invoke(_currentStunLevel, _maxStunLevel);

        }
    }
}