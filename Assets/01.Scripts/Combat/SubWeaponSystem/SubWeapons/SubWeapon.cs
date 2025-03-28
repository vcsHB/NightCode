using System;
using UnityEngine;
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
        public event Action<float, float> OnCooltimeChangeEvent;
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
        }
    }
}