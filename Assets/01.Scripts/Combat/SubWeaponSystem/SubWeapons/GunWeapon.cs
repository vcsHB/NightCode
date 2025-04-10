using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;
namespace Combat.SubWeaponSystem
{
    public class GunWeapon : SubWeapon
    {
        [SerializeField] private bool _isContinueFire;
        public UnityEvent OnFireEvent;
        [SerializeField] private float _projectileSpread;
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private float _fireCooltime;
        private float _lastFireTime;
        public bool IsCooltimeOver => _lastFireTime + _fireCooltime < Time.time;

        private bool _isShooting;
        private Vector2 _direction;

        public override void UseWeapon(SubWeaponControlData data)
        {
            if (!CheckEnoughCount(_requireCount)) return;
            _isShooting = true;
            _direction = data.direction;
            if (!_isContinueFire)
            {
                if (IsCooltimeOver)
                {
                    ReduceCount(_requireCount);
                    Shoot(data.direction);

                }
            }
        }

        public override void CancelWeapon()
        {
            base.CancelWeapon();
            _isShooting = false;

        }


        protected override void Update()
        {
            //base.Update();
            if (_isContinueFire && _isShooting)
            {
                if (!CheckEnoughCount(_requireCount)) return;
                if (!IsCooltimeOver) return;
                Shoot(_direction);
                ReduceCount(_requireCount);
                _lastFireTime = Time.time;
            }
        }

        protected void Shoot(Vector2 direction)
        {
            direction = direction + (Random.insideUnitCircle * _projectileSpread);
            direction.Normalize();
            _shooter.SetDirection(direction);
            _shooter.FireProjectile();
            OnFireEvent?.Invoke();
        }
    }
}