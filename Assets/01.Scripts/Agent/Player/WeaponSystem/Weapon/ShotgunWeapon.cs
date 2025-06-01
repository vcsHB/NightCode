using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Players.WeaponSystem.Weapon
{

    public class ShotgunWeapon : PlayerWeapon
    {
        public UnityEvent OnFireEvent;
        [SerializeField] private ShotgunProjectileShooter _projectileShooter;
        [SerializeField] private TargetDetector _targetDetector;

        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnGroundPullArriveEvent.AddListener(HandleAttack);
        }

        public override void HandleAttack()
        {
            Collider2D target = _targetDetector.DetectClosestTarget();
            if (target == null)
                return;
            Vector2 direction = target.transform.position - transform.position;
            _projectileShooter.FireShotgun(direction);
            OnFireEvent?.Invoke();
        }
    }
}