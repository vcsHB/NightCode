using System;
using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnHealthChangedEvent;
        public UnityEvent OnDieEvent;
        public event Action<float, float> OnHealthChangedValueEvent;

        public float MaxHealth => _maxHealth;
        [SerializeField]
        private float _maxHealth;
        [SerializeField]
        private float _currentHealth = 0;

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

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            CheckDie();
            HandleHealthChanged();
        }

        public void Restore(float amount)
        {
            _currentHealth += amount;
            HandleHealthChanged();

        }
        private void CheckDie()
        {
            if(_currentHealth <= 0)
            {
                OnDieEvent?.Invoke();
            }
        }

        private void HandleHealthChanged()
        {
            OnHealthChangedValueEvent?.Invoke(_currentHealth, MaxHealth);
            OnHealthChangedEvent?.Invoke();
        }

    }
}