using Combat;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.Obstacles
{

    public class WallTurret : MonoBehaviour
    {
        public UnityEvent OnFireEvent;
        [SerializeField] private ProjectileShooter _shooter;

        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _aimObstacleTargetLayer;
        [SerializeField] private Vector2 _detectOffset;
        [SerializeField] private float _targetDetectRadius;

        [SerializeField] private float _fireCooltime;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Transform _headTrm;
        [SerializeField] private SpriteRenderer _headVisualRenderer;
        [SerializeField] private LineRenderer _aimDirectionRenderer;
        [SerializeField] private TurretAim _turretAim;
        [SerializeField] private float _aimCheckSize;
        [SerializeField] private float _bulletRandomize = 0.2f;
        private Collider2D _collider;

        private Collider2D _target;
        public bool IsTargetDetected => _target != null;
        private float _lastFireTime;
        [Header("Sprite Setting")]
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private bool _isActive = true;
        private Health _healthCompo;

        private void Awake()
        {
            _healthCompo = GetComponent<Health>();
            _collider = GetComponent<Collider2D>();
            _healthCompo.OnDieEvent.AddListener(HandleDie);
            //SetSprite(_isActive);
        }

        private void HandleDie()
        {
            _isActive = false;
            SetEnabled(_isActive);
        }

        private void Update()
        {
            if (_isActive)
            {
                DetectTarget();
                RotateHead();
                TryShoot();

            }
        }

        private void TryShoot()
        {
            Vector2 direction = _shooter.transform.up;
            RaycastHit2D obstacle = Physics2D.Raycast((Vector2)transform.position + _detectOffset, direction, _targetDetectRadius, _aimObstacleTargetLayer);
            if (obstacle.collider != null)
            {
                RefreshAimLine(obstacle.point);
            }
            else
            {
                RefreshAimLine((Vector2)transform.position + (direction.normalized * _targetDetectRadius));
            }
            if (!IsTargetDetected) return;
            if (_lastFireTime + _fireCooltime > Time.time) return;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _aimCheckSize, direction, _targetDetectRadius, _targetLayer);

            if (hit.collider != null)
            {
                _lastFireTime = Time.time;
                OnFireEvent?.Invoke();
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

        private void SetEnabled(bool value)
        {
            _collider.enabled = value;
            _headVisualRenderer.sprite = value ? _enabledSprite : _disabledSprite;
            _turretAim.SetActive(value);
            _aimDirectionRenderer.enabled = value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRadius);
        }

        private void RefreshAimLine(Vector2 targetPosition)
        {
            _aimDirectionRenderer.SetPosition(0, transform.position);
            _aimDirectionRenderer.SetPosition(1, targetPosition);
            _turretAim.SetPosition(targetPosition);
        }

    }
}