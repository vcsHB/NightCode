using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Enemies.Highbinders
{

    public class HighbinderShooterController : EnemyAttackController
    {
        [SerializeField] private ProjectileShooter _shooter;

        public override void Attack()
        {
            Vector2 targetPosition = _targetVariable.Value.position;
            Vector2 direction = targetPosition - (Vector2)_owner.transform.position;
            Debug.Log(direction);
            _shooter.SetDirection(direction + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
            _shooter.SetDirection(direction + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
        }

    }
}