using Core.Attribute;
using ObjectPooling;
using UnityEngine;

namespace Combat.CombatObjects.ProjectileManage
{
    public class ShotgunProjectileShooter : MonoBehaviour
    {
        [SerializeField] private PoolingType _projectilePoolType;
        [SerializeField] private ProjectileData _baseProjectileData;

        [Header("Shotgun Settings")]
        [SerializeField] private int _projectileCount = 5;
        [SerializeField] private float _spreadAngle = 30f; // 
        [SerializeField] private bool _useRandomSpeed = false;
        [ShowIf(nameof(_useRandomSpeed)), SerializeField] private float _minSpeed = 5f;
        [ShowIf(nameof(_useRandomSpeed)), SerializeField] private float _maxSpeed = 10f;


        public void SetDirection(Vector2 newDirection)
        {
            _baseProjectileData.direction = newDirection;
        }
        
        public void FireShotgun(Vector2 direction)
        {
            Vector2 baseDir = direction.normalized;
            float halfAngle = _spreadAngle * 0.5f;

            for (int i = 0; i < _projectileCount; i++)
            {
                float t = (_projectileCount <= 1) ? 0.5f : (float)i / (_projectileCount - 1);
                float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
                float rad = angle * Mathf.Deg2Rad;

                Vector2 rotatedDirection = RotateVector(baseDir, rad);

                Projectile projectile = PoolManager.Instance.Pop(_projectilePoolType) as Projectile;
                projectile.transform.position = transform.position;

                ProjectileData newData = _baseProjectileData;
                newData.direction = rotatedDirection;

                if (_useRandomSpeed)
                {
                    newData.speed = Random.Range(_minSpeed, _maxSpeed);
                }

                projectile.Shoot(newData);
            }
        }

        private Vector2 RotateVector(Vector2 vector, float radians)
        {
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);

            return new Vector2(
                vector.x * cos - vector.y * sin,
                vector.x * sin + vector.y * cos
            );

        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.red;
            Vector2 baseDir = transform.right.normalized;
            float halfAngle = _spreadAngle * 0.5f;

            for (int i = 0; i < _projectileCount; i++)
            {
                float t = (_projectileCount <= 1) ? 0.5f : (float)i / (_projectileCount - 1);
                float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
                float rad = angle * Mathf.Deg2Rad;

                Vector2 rotatedDir = RotateVector(baseDir, rad);
                Gizmos.DrawRay(transform.position, rotatedDir);
            }
        }
#endif

    }
}