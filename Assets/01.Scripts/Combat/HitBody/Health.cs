using System;
using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnHealthChangedEvent;
        public UnityEvent OnDieEvent;
        public UnityEvent OnReviveEvent;
        public event Action<float, float> OnHealthChangedValueEvent;
        public event Action<CombatData> OnHitCombatDataEvent;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth = 0;
        [SerializeField] private float _hitResistanceCooltime = 0.15f;
        private float _lastHitTime;
        public bool IsResist { get; private set; }
        private bool _isDie;
        public bool IsDead => _isDie;

        public void Initialize(float health)
        {
            _maxHealth = health;
            SetMaxHealth();
        }

        private void SetMaxHealth()
        {
            _currentHealth = MaxHealth;
            HandleHealthChanged();
        }

        public bool ApplyDamage(CombatData data)
        {
            //Debug.Log(data.invalidityResistance);
            if (!data.invalidityResistance && _lastHitTime + _hitResistanceCooltime > Time.time) return false;
            if (IsResist) return false;
            _currentHealth -= data.damage;
            _lastHitTime = Time.time;
            OnHitCombatDataEvent?.Invoke(data);
            CheckDie();
            HandleHealthChanged();
            return true;
        }

        public void Restore(float amount)
        {
            _currentHealth += amount;
            HandleHealthChanged();

        }

        public void Revive()
        {
            _isDie = false;
            OnReviveEvent?.Invoke();
        }

        private void CheckDie()
        {
            if(_isDie) return;
            if (_currentHealth <= 0)
            {
                _isDie = true;
                OnDieEvent?.Invoke();
            }
        }

        private void HandleHealthChanged()
        {
            OnHealthChangedValueEvent?.Invoke(_currentHealth, MaxHealth);
            OnHealthChangedEvent?.Invoke();
        }

        public void SetResist(bool value)
        {
            IsResist = value;
        }

    }
}