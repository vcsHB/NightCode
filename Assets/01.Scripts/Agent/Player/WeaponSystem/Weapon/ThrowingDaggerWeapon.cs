using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon
{

    public class ThrowingDaggerWeapon : PlayerWeapon
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _throwTerm;
        [SerializeField] private int _throwAmount;
        [SerializeField] private PoolingProjectileShooter _shooter;
        private bool _isThrowing;
        private float _lastThrowTime;
        private int _currentThrowAmount = 0;

        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnRopeTurboEvent.AddListener(HandleAttack);
            _animationTrigger.OnGroundLandEvent.AddListener(HandleGroundLand);
        }

        private void Update()
        {

            if (_currentThrowAmount < _throwAmount && _isThrowing)
            {

                if (_lastThrowTime + _throwTerm < Time.time)
                {
                    FireProjectile();
                }
            }

        }

        private void FireProjectile()
        {
            Collider2D target = _targetDetector.DetectClosestTarget();
            if (target == null) return;
            Vector2 direction = target.transform.position - transform.position;
            _lastThrowTime = Time.time;
            ThrowingDagger dagger = _shooter.GetProjectile() as ThrowingDagger;
            dagger.transform.position = transform.position;
            dagger.Fire(direction);
            _currentThrowAmount++;
        }


        public override void HandleAttack()
        {
            _isThrowing = true;
            _currentThrowAmount = 0;
        }

        private void HandleGroundLand()
        {
            _isThrowing = false;
        }
    }
}