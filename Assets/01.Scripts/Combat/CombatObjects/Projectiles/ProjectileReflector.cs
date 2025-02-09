using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileReflector : ProjectileWallDestroyer
    {
        [SerializeField] private int _reflectAmount = 1;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallDetectDistance = 1f;
        [SerializeField, Range(0f, 1f)] private float _ReflectSpeedMultipler = 1f;


        private int _currentReflectCount = 0;


        public override void OnCollision()
        {
            if(_currentReflectCount >= _reflectAmount)
            {
                base.OnCollision();
                return;
            }
            Vector2 previousVelocity = _owner.Velocity;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, previousVelocity.normalized, _wallDetectDistance, _wallLayer);
            if (!hit) return;
            float speed = previousVelocity.magnitude * _ReflectSpeedMultipler;
            Vector2 reflectVelocity = Vector2.Reflect(previousVelocity, hit.normal).normalized * speed;
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