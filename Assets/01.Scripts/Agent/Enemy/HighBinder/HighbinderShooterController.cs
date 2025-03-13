using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Enemies.Highbinders
{

    public class HighbinderShooterController : EnemyAttackController
    {
        [SerializeField] private ProjectileShooter _shooter;



        public override void Attack()
        {
            Vector2 targetPosition = _owner.GetVariable<Transform>("Target").Value.position;
            Vector2 direction = targetPosition - (Vector2)_owner.transform.position;
            _shooter.SetDirection(direction * Random.insideUnitSphere);
            _shooter.FireProjectile();
        }

    }
}