using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileReflector : ProjectileWallDestroyer
    {
        [SerializeField] private int _reflectAmount = 1;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallDetectDistance = 1f;
        [SerializeField, Range(0f, 3f)] private float _reflectSpeedMultipler = 1f;


        private int _currentReflectCount = 0;


        public override void OnCollision()
        {
            if(_currentReflectCount >= _reflectAmount)
            {
                base.OnCollision();
                return;
            }
            _currentReflectCount++;
            Vector2 previousVelocity = _owner.Velocity;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, previousVelocity.normalized, _wallDetectDistance, _wallLayer);
            if (hit.collider == null) return;
            float speed = previousVelocity.magnitude * _reflectSpeedMultipler;
            Vector2 reflectVelocity = Vector2.Reflect(previousVelocity.normalized, hit.normal).normalized * speed;
            _owner.SetVelocity(reflectVelocity);
        }

        public override void OnGenerated()
        {
            base.OnGenerated();
            _currentReflectCount = 0;
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