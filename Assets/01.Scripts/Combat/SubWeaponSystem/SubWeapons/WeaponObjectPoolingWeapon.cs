using System.Collections.Generic;
using UnityEngine;
namespace Combat.SubWeaponSystem
{

    public abstract class WeaponObjectPoolingWeapon : SubWeapon
    {
        [SerializeField] private PoolableWeaponObject _weaponObjectPrefab;
        [SerializeField] private int _defaultPoolAmount = 10;
        private Queue<PoolableWeaponObject> _weaponObjectPool = new();


        public PoolableWeaponObject GetNewWeaponObject()
        {
            PoolableWeaponObject newObejct = _weaponObjectPool.Count > 0 ?
                _weaponObjectPool.Dequeue() :
                Instantiate(_weaponObjectPrefab);

            newObejct.OnDestroyEvent += HandleWeaponObjectDestroyPooling;
            newObejct.ResetObject();
            return newObejct;
        }

        protected virtual void HandleWeaponObjectDestroyPooling(PoolableWeaponObject weaponObject)
        {
            weaponObject.OnDestroyEvent -= HandleWeaponObjectDestroyPooling;
            _weaponObjectPool.Enqueue(weaponObject);
        }

    }
}