using System;
using UnityEngine;
namespace Combat.SubWeaponSystem
{
    public struct SubWeaponControlData
    {
        public Vector2 direction;
        public float damage;
        public float speed;
    }

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