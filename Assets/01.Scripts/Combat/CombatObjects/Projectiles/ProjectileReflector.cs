using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileReflector : MonoBehaviour, IProjectileComponent
    {
        [SerializeField] private int _reflectAmount = 1;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallDetectDistance = 1f;
        private Projectile _owner;
        [SerializeField, Range(0f, 1f)] private float _ReflectSpeedMultipler = 1f;

        void IProjectileComponent.Initialize(Projectile projectile)
        {
            _owner = projectile;
        }

        void IProjectileComponent.OnCasted()
        {

        }

        void IProjectileComponent.OnCollision()
        {
            Vector2 previousVelocity = _owner.Velocity;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, previousVelocity.normalized, _wallDetectDistance, _wallLayer);
            if (!hit) return;
            float speed = previousVelocity.magnitude * _ReflectSpeedMultipler;
            Vector2 reflectVelocity = Vector2.Reflect(previousVelocity, hit.normal).normalized * speed;
            _owner.SetVelocity(reflectVelocity);
        }

        void IProjectileComponent.OnGenerated()
        {

        }

        void IProjectileComponent.OnProjectileDamaged()
        {

        }

        void IProjectileComponent.OnProjectileDestroy()
        {

        }

        void IProjectileComponent.OnShot()
        {

        }

        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _wallDetectDistance);
        }

#endif
    }
}