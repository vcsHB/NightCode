using System;
using System.Collections.Generic;
using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using UnityEngine;

namespace Agents.Players.WeaponSystem.Weapon
{
    public class RocketLauncherWeapon : PlayerWeapon
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private RangeWeaponAimVisual _rangeWeaponVisual;
        [SerializeField] private RocketProjectile _rocketPrefab;
        private Queue<RocketProjectile> _rocketPool = new();
        private Transform _targetTrm;
        private bool _isAiming;


        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnRopeTurboEvent.AddListener(HandleAttack);
            _animationTrigger.OnRopeShootEvent.AddListener(HandleRopeShoot);
            _animationTrigger.OnRopeRemoveEvent.AddListener(HandleRopeRemove);
        }

        private void HandleRopeRemove()
        {
            _rangeWeaponVisual.SetAimEnable(false);
            _isAiming = false;
        }

        private void HandleRopeShoot()
        {
            _rangeWeaponVisual.SetAimEnable(true);
            _isAiming = true;

        }

        private void Update()
        {
            if (_isAiming)
            {
                Collider2D target = _targetDetector.DetectClosestTarget();
                if (target == null)
                {
                    _targetTrm = null;
                }
                _targetTrm = target.transform;
                _rangeWeaponVisual.SetAimToTarget(_targetTrm);

            }
        }

        public override void HandleAttack()
        {

            if (!_isAiming || _targetTrm == null)
            {
                return;
            }
            RocketProjectile rocket = GetRocket();
            rocket.transform.position = transform.position;
            Vector2 direction = _targetTrm.position - transform.position;
            rocket.Fire(direction);
        }

        private RocketProjectile GetRocket()
        {
            RocketProjectile rocket = _rocketPool.Count > 0 ?
                _rocketPool.Dequeue() :
                Instantiate(_rocketPrefab);
            rocket.OnRocketReturnEvent += HandleRocketDestroy;
            return rocket;

        }

        private void HandleRocketDestroy(RocketProjectile rocket)
        {
            rocket.OnRocketReturnEvent -= HandleRocketDestroy;
            _rocketPool.Enqueue(rocket);
        }
    }
}