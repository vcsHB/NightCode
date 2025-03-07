using System;
using Combat.Casters;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.CombatObjects.ProjectileManage
{
    public class Projectile : MonoBehaviour, IPoolable, IDamageable
    {
        public UnityEvent OnShotSerializedEvent;
        protected Collider2D _collider;
        protected Caster _caster;
        protected Transform _visualTrm;
        protected Rigidbody2D _rigidCompo;
        private bool _isActive;
        private float _currentLifeTime = 0f;
        [SerializeField] private ProjectileData _projectileData;
        [field:SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;
        private IProjectileComponent[] _projectileComponents;
        private float _defaultGravityScale;

        // internal Properties
        internal Vector2 Velocity => _rigidCompo.linearVelocity;


        // Events
        public event Action OnGeneratedEvent;
        public event Action OnCastEvent;
        public event Action OnShotEvent;
        public event Action OnCollisionEvent;
        public event Action OnDestroyEvent;
        public event Action OnDamagedEvent;


        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
            _defaultGravityScale = _rigidCompo.gravityScale;
            _collider = GetComponent<Collider2D>();
            _caster = GetComponentInChildren<Caster>();
            _visualTrm = transform.Find("Visual");
            _caster.OnCastSuccessEvent.AddListener(HandleCastSuccessed);

            InitComponents();
        }

        private void InitComponents()
        {
            _projectileComponents = GetComponentsInChildren<IProjectileComponent>();

            foreach (IProjectileComponent compo in _projectileComponents)
            {
                compo.Initialize(this);
                OnGeneratedEvent += compo.OnGenerated;
                OnCastEvent += compo.OnCasted;
                OnShotEvent += compo.OnShot;
                OnCollisionEvent += compo.OnCollision;
                OnDamagedEvent += compo.OnProjectileDamaged;
                OnDestroyEvent += compo.OnProjectileDestroy;
            }

        }

        private void OnDestroy()
        {
            foreach (IProjectileComponent compo in _projectileComponents)
            {
                OnGeneratedEvent -= compo.OnGenerated;
                OnCastEvent -= compo.OnCasted;
                OnShotEvent -= compo.OnShot;
                OnCollisionEvent -= compo.OnCollision;
                OnDamagedEvent -= compo.OnProjectileDamaged;
                OnDestroyEvent -= compo.OnProjectileDestroy;
            }
        }
        private void Update()
        {
            if (!_isActive) return;
            _currentLifeTime += Time.deltaTime;
            _caster.Cast();
            if(_currentLifeTime >= _projectileData.lifeTime)
            {
                _currentLifeTime = 0f;
                HandleDestroy();
            }

        }

        private void HandleCastSuccessed()
        {
            if (_isActive)
            {
                if (_projectileData.canPenetrate) return;
                HandleDestroy();
            }
        }
        [ContextMenu("DebugShoot")]
        private void DebugShoot()
        {
            Shoot(_projectileData);
        }
        #region External Functions

        internal void SetVelocity(Vector2 velocity) => _rigidCompo.linearVelocity = velocity;
        public void Shoot(Vector2 direction)
        {
            _projectileData.direction = direction;
            Shoot(_projectileData);
        }

        public void Shoot(ProjectileData data)
        {
            _isActive = true;
            _projectileData = data;
            _visualTrm.right = data.direction;
            _caster.SendCasterData(new DamageCasterData() {damage = data.damage});
            _rigidCompo.linearVelocity = data.direction.normalized * data.speed;
            OnShotEvent?.Invoke();
            OnShotSerializedEvent?.Invoke();
        }

        public void ResetItem()
        {
            _currentLifeTime = 0f;
            OnGeneratedEvent?.Invoke();
        }

        #endregion
        internal void SetGravityMultiplier(float gravity) => _rigidCompo.gravityScale = gravity;
        internal void ResetGravityMultiplier() => _rigidCompo.gravityScale = _defaultGravityScale;

        internal void HandleDestroy()
        {
            _isActive = false;
            OnDestroyEvent?.Invoke();
            PoolManager.Instance.Push(this);
            //Destroy(gameObject);
        }

        public void ApplyDamage(CombatData data)
        {
            if (!_projectileData.canDestroy) return;
            OnDamagedEvent?.Invoke();
            HandleDestroy();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollisionEvent?.Invoke();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_rigidCompo == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Velocity);
        }
#endif
    }

}