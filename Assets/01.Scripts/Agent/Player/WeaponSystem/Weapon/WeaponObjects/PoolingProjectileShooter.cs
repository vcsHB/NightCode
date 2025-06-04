using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Projectile = Agents.Players.WeaponSystem.Weapon.WeaponObjects.WeaponPoolingProjectileObject;
// Using Keyword summation
// It is not Combat.CombatObjects.Projectile

namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class PoolingProjectileShooter : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _initPoolAmount = 10;

        private Queue<Projectile> _pool;

        private void Awake()
        {
            _pool = new Queue<Projectile>(_initPoolAmount);
            Initialize();
        }

        public Projectile GetProjectile()
        {
            Projectile projectile = _pool.Count > 0 ?
                _pool.Dequeue() :
                Instantiate(_projectilePrefab);
            projectile.OnProjectileReturnEvent += HandleProjectileReturned;
            projectile.ResetProjectile();
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        private void HandleProjectileReturned(Projectile projectile)
        {
            _pool.Enqueue(projectile);
            projectile.OnProjectileReturnEvent -= HandleProjectileReturned;
            projectile.gameObject.SetActive(false);
        }

        private void Initialize()
        {
            for (int i = 0; i < _initPoolAmount; i++)
            {
                Projectile projectile = Instantiate(_projectilePrefab);
                _pool.Enqueue(projectile);
            }
        }
    }
}