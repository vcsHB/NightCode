using Agents;
using Combat;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.Obstacles
{
    public class WallTurret : Agent
    {
        public UnityEvent OnFireEvent;

        [Header("Shooter Settings")]
        [SerializeField] private ProjectileShooter _shooter;

        [Header("Targeting Settings")]
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _aimObstacleTargetLayer;
        [SerializeField] private Vector2 _detectOffset;
        [SerializeField] private float _targetDetectRadius;
        [SerializeField] private float _aimCheckSize;
        [SerializeField] private float _bulletRandomize = 0.2f;

        [Header("Fire Control")]
        [SerializeField] private float _fireCooltime = 0.2f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _fireDuration = 2f;
        [SerializeField] private float _restDuration = 2f;

        [Header("Visual Components")]
        [SerializeField] private Transform _headTrm;
        [SerializeField] private SpriteRenderer _headVisualRenderer;
        [SerializeField] private LineRenderer _aimDirectionRenderer;
        [SerializeField] private TurretAim _turretAim;

        [Header("Sprite Setting")]
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private bool _isActive = true;

        private Collider2D _collider;
        private Collider2D _target;
        private Health _healthCompo;
        private float _lastFireTime;

        private bool _isFiringPhase = true;
        private float _phaseStartTime;

        public bool IsTargetDetected => _target != null;

        protected override void Awake()
        {
            base.Awake();
            _healthCompo = GetComponent<Health>();
            _collider = GetComponent<Collider2D>();
            _healthCompo.OnDieEvent.AddListener(HandleDie);
            _phaseStartTime = Time.time;
        }

        private void HandleDie()
        {
            _isActive = false;
            SetEnabled(_isActive);
        }

        private void Update()
        {
            if (!_isActive) return;

            float currentPhaseDuration = _isFiringPhase ? _fireDuration : _restDuration;
            if (Time.time - _phaseStartTime > currentPhaseDuration)
            {
                _isFiringPhase = !_isFiringPhase;
                _phaseStartTime = Time.time;
            }

            DetectTarget();
            RotateHead();

            if (_isFiringPhase)
                TryShoot();
            else
                RefreshAimLine(transform.position + (_shooter.transform.up.normalized * _targetDetectRadius));
        }

        private void TryShoot()
        {
            if (!IsTargetDetected) return;
            if (_lastFireTime + _fireCooltime > Time.time) return;

            Vector2 direction = GetDirection().normalized;

            RaycastHit2D obstacle = Physics2D.Raycast((Vector2)transform.position + _detectOffset, direction, _targetDetectRadius, _aimObstacleTargetLayer);
            Vector2 aimPoint = obstacle.collider != null
                ? obstacle.point
                : (Vector2)transform.position + direction * _targetDetectRadius;
            RefreshAimLine(aimPoint);

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
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            _headTrm.localRotation = Quaternion.Lerp(_headTrm.localRotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }

        private Vector2 GetDirection()
        {
            return IsTargetDetected
                ? (Vector2)(_target.transform.position - transform.position)
                : Vector2.up;
        }

        private void SetEnabled(bool value)
        {
            _collider.enabled = value;
            _headVisualRenderer.sprite = value ? _enabledSprite : _disabledSprite;
            _turretAim.SetActive(value);
            _aimDirectionRenderer.enabled = value;
        }

        private void RefreshAimLine(Vector2 targetPosition)
        {
            _aimDirectionRenderer.SetPosition(0, transform.position);
            _aimDirectionRenderer.SetPosition(1, targetPosition);
            _turretAim.SetPosition(targetPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRadius);
        }
    }
}
