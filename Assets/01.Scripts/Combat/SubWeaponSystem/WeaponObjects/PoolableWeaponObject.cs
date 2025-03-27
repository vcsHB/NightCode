using System;
namespace Combat.SubWeaponSystem
{
    public abstract class PoolableWeaponObject : WeaponObject
    {
        public event Action<PoolableWeaponObject> OnDestroyEvent;
        protected bool _isActive;
        public bool IsActive => _isActive;
        public virtual void ResetObject()
        {
            _isActive = true;
        }
        public virtual void Destroy()
        {
            _isActive = false;
            OnDestroyEvent?.Invoke(this);
        }
    }
}