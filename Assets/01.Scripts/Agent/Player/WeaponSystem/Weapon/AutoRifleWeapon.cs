using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Players.WeaponSystem.Weapon
{

    public class AutoRifleWeapon : PlayerWeapon
    {
        public UnityEvent OnFireEvent;
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private RangeWeaponAimVisual _rangeWeaponVisual;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _fireTerm = 0.1f;
        [SerializeField] private int _fireAmount = 5;
        private int _currentFireAmount = 0;

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
            _rangeWeaponVisual.SetAimEnable(false);
        }

        public override void HandleAttack()
        {
            _isShooting = true;
            _currentFireAmount = 0;
        }

        private void Update()
        {
            if (_isShooting)
            {

                _targetCollider = _targetDetector.DetectClosestTarget();
                if (_targetCollider == null)
                {
                    HandleRemoveRope();
                    return;
                }
                _rangeWeaponVisual.SetAimEnable(true);
                Vector2 direction = _targetCollider.transform.position - transform.position;
                _rangeWeaponVisual.SetAimToTarget(_targetCollider.transform);
                if (_lastFireTime + _fireTerm < Time.time)
                {
                    _shooter.SetDirection(direction);
                    _shooter.FireProjectile();
                    OnFireEvent?.Invoke();
                    _currentFireAmount++;
                    _lastFireTime = Time.time;
                    if (_currentFireAmount >= _fireAmount)
                    {
                        HandleRemoveRope();
                    }
                }
            }
        }
    }

}