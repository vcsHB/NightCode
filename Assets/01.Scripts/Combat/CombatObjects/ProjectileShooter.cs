using ObjectPooling;
using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileShooter : MonoBehaviour
    {
        [SerializeField] private PoolingType _projectilePoolType;
        [SerializeField] private ProjectileData _projectileData;


        public void SetDirection(Vector2 newDirection)
        {
            _projectileData.direction = newDirection;
        }
        public void FireProjectile()
        {
            Projectile projectile = PoolManager.Instance.Pop(_projectilePoolType) as Projectile;
            projectile.transform.position = transform.position;
            projectile.Shoot(_projectileData);
        }
        public void FireProjectile(Vector2 direction)
        {
            Projectile projectile = PoolManager.Instance.Pop(_projectilePoolType) as Projectile;
            projectile.transform.position = transform.position;
            projectile.Shoot(direction);
        }
    }
}