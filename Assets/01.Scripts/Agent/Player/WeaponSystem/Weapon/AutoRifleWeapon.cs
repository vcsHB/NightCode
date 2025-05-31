using System;
using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.CombatObjects.ProjectileManage;
using ObjectPooling;
using UnityEngine;

namespace Agents.Players.WeaponSystem.Weapon
{

    public class AutoRifleWeapon : PlayerWeapon
    {
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private RangeWeaponAimVisual _rangeWeaponVisual;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _fireTerm = 0.1f;
        [SerializeField] 
        private float _lastFireTime;
        private bool _isShooting;
        private Collider2D _targetCollider;

        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnRopeTurboEvent.AddListener(HandleAttack);
            _animationTrigger.OnRopeRemoveEvent.AddListener(HandleRemoveRope);
        }

        private void HandleRemoveRope()
        {
            _isShooting = false;
        }

        public override void HandleAttack()
        {
            _isShooting = true;
        }
        
        private void Update()
        {
            if (_isShooting)
            {
                _targetCollider = _targetDetector.DetectTarget();
                if (_targetCollider == null) return;
                Vector2 direction = _targetCollider.transform.position - transform.position;

                if (_lastFireTime + _fireTerm < Time.time)
                {
                    _shooter.SetDirection(direction);
                    _shooter.FireProjectile();
                }
            }
        }
    }

}