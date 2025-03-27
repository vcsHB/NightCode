using System;
using UnityEngine;
namespace Combat.SubWeaponSystem
{

    public abstract class PoolableWeaponObject : WeaponObject
    {
        public event Action<PoolableWeaponObject> OnDestroyEvent;
        public virtual void ResetObject()
        {

        }
        public virtual void Destroy()
        {
            OnDestroyEvent?.Invoke(this);
        }
    }
}