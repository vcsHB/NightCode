using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Enemies.Highbinders
{

    public class HighbinderShooterController : EnemyAttackController
    {
        [SerializeField] private ProjectileShooter _shooter;

        public override void Attack()
        {
            if (_targetVariable == null) return;
            if (_targetVariable.Value == null) return;
            Vector2 targetPosition = _targetVariable.Value.position;
            Vector2 direction = targetPosition - (Vector2)_owner.transform.position;
            _shooter.SetDirection(direction + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
            _shooter.SetDirection(direction + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
            InvokeAttackEnd();
        }

    }
}