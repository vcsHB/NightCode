using Combat.CombatObjects.ProjectileManage;
using UnityEngine;

namespace ObjectManage.Obstacles
{

    public class WallTurret : MonoBehaviour
    {
        [SerializeField] private ProjectileShooter _shooter;

        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _targetDetectRadius;

        [SerializeField] private float _fireCooltime;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Transform _headTrm;
        [SerializeField] private float _aimCheckSize;
        [SerializeField] private float _bulletRandomize = 0.2f;

        private Collider2D _target;
        public bool IsTargetDetected => _target != null;
        private float _lastFireTime;

        private void Update()
        {
            DetectTarget();
            RotateHead();
            TryShoot();

        }

        private void TryShoot()
        {
            if (!IsTargetDetected) return;
            if(_lastFireTime + _fireCooltime > Time.time) return;
            Vector2 direction = _shooter.transform.up;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _aimCheckSize, direction, _targetDetectRadius, _targetLayer);

            if(hit.collider != null)
            {
                _lastFireTime = Time.time;
                _shooter.FireProjectile(direction + (Random.insideUnitCircle * _bulletRandomize));
            }
        }

        private void DetectTarget()
        {
            _target = Physics2D.OverlapCircle(transform.position, _targetDetectRadius, _targetLayer);
        }

        private void RotateHead()
        {
            if (!IsTargetDetected) return;
            Vector2 direction = GetDirection().normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Lerp(_headTrm.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * _rotationSpeed);
            _headTrm.localRotation = rotation;
        }

        private Vector2 GetDirection()
        {
            if (!IsTargetDetected) return Vector2.zero;

            return _target.transform.position - transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRadius);
        }

    }
}