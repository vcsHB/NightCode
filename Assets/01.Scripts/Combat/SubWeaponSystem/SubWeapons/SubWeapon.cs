using System;
using UnityEngine;
using UnityEngine.Events;
namespace Combat.SubWeaponSystem
{
    /// <summary>
    /// public Weapon Contol data Group structure.
    /// Unused Properties may exist. Use it as needed.
    /// </summary>
    public struct SubWeaponControlData
    {
        public Vector2 direction;
        public float damage;
        public float speed;
    }
    /// <summary>
    /// basic Weapon Manage Controller. (AgentComponent)
    /// manage relationship order
    /// [WeaponController] -> Weapon -> WeaponObject
    /// </summary>
    public abstract class SubWeapon : MonoBehaviour
    {
        public UnityEvent OnUsedEvent;
        public UnityEvent OnUseCancelEvent;
        public event Action<int, int> OnWeaponCountChange;
        public event Action<float, float> OnCooltimeChangeEvent;
        [SerializeField] protected int _maxAttackAmount;
        [SerializeField] protected int _leftAttackCount;
        [SerializeField] protected int _requireCount = 1;

        [SerializeField] protected float _useCooltime;
        protected float _currentCoolTime;
        public bool CanUse => _currentCoolTime >= _useCooltime;

        protected virtual void Update()
        {
            if (!CanUse)
                _currentCoolTime += Time.deltaTime;
            OnCooltimeChangeEvent?.Invoke(_currentCoolTime, _useCooltime);

        }

        public virtual void UseWeapon(SubWeaponControlData data)
        {
            _currentCoolTime = 0f;
            OnUsedEvent?.Invoke();
        }

        public virtual void CancelWeapon()
        {
            OnUseCancelEvent?.Invoke();
        }

        protected void ReduceCount(int amount)
        {
            _leftAttackCount -= amount;
            OnWeaponCountChange?.Invoke(_leftAttackCount, _maxAttackAmount);
        }
        public bool CheckEnoughCount(int amount) => _leftAttackCount >= amount;

    }
}