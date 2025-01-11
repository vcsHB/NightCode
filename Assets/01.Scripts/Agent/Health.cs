using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnHealthChangedEvent;
        public UnityEvent OnDieEvent;

        public float MaxHealth => _maxHealth;
        private float _maxHealth;
        private float _currentHealth = 0;

        public void Initialize(float health)
        {
            _maxHealth = health;
            SetMaxHealth();
        }
        
        private void SetMaxHealth()
        {
            _currentHealth = MaxHealth;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;
            CheckDie();
        }

        public void Restore(float amount)
        {
            _currentHealth += amount;

        }
        private void CheckDie()
        {
            if(_currentHealth <= 0)
            {
                OnDieEvent?.Invoke();
            }
        }

    }
}