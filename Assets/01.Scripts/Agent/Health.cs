using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnHealthChangedEvent;
        public UnityEvent OnDieEvent;

        public int MaxHealth => _maxHealth;
        private int _maxHealth;
        private int _currentHealth = 0;

        public void Initialize(int health)
        {
            _maxHealth = health;
            SetMaxHealth();
        }
        
        private void SetMaxHealth()
        {
            _currentHealth = MaxHealth;
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            CheckDie();
        }

        public void Restore(int amount)
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