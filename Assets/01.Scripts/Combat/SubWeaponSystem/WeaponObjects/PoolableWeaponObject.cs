using System;
namespace Combat.SubWeaponSystem
{
    public abstract class PoolableWeaponObject : WeaponObject
    {
        public event Action<PoolableWeaponObject> OnDestroyEvent;
        protected bool _isActive;
        public bool IsActive => _isActive;

        /// <summary>
        /// Call at Active, and Enqueue in Pool 
        /// </summary>
        public virtual void ResetObject()
        {
            _isActive = true;
        }

        /// <summary>
        /// Call at Disabl, and return to Pooling Queue
        /// </summary>
        public virtual void Destroy()
        {
            _isActive = false;
            OnDestroyEvent?.Invoke(this);
        }
    }
}