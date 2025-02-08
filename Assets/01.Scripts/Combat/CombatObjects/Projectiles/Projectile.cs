using System;
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
        [SerializeField] private ProjectileData _projectileData;
        public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;
        private IProjectileComponent[] _projectileComponents;


        // Events
        public event Action OnGeneratedEvent;
        public event Action OnCastEvent;
        public event Action OnShotEvent;
        public event Action OnDestroyEvent;
        public event Action OnDamagedEvent;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _caster = GetComponent<Caster>();
            _caster.OnCastSuccessEvent.AddListener(HandleCastSuccessed);

            InitComponents();
        }

        private void InitComponents()
        {
            _projectileComponents = GetComponentsInChildren<IProjectileComponent>();
            foreach (IProjectileComponent compo in _projectileComponents)
            {
                compo.Initialize(this);
                OnCastEvent += compo.OnCasted;
                OnDamagedEvent += compo.OnProjectileDamaged;
            }

        }

        private void OnDestroy()
        {
            foreach (IProjectileComponent compo in _projectileComponents)
            {
                OnCastEvent -= compo.OnCasted;
                OnDamagedEvent -= compo.OnProjectileDamaged;
            }
        }

        private void HandleCastSuccessed()
        {
            if (_isActive)
            {
                HandleDestroy();
            }
        }
        public void Shoot()
        {
            Shoot(_projectileData);
        }

        public void Shoot(ProjectileData data)
        {
            _isActive = true;
            _visualTrm.right = data.direction;
            OnShotEvent?.Invoke();
            OnShotSerializedEvent?.Invoke();

        }

        private void Update()
        {
            if (!_isActive) return;
            _caster.Cast();

        }

        public void ResetItem()
        {
            OnGeneratedEvent?.Invoke();
        }

        private void HandleDestroy()
        {
            _isActive = false;
            OnDestroyEvent?.Invoke();
            PoolManager.Instance.Push(this);

        }

        public void ApplyDamage(float damage)
        {
            if (!_projectileData.canDestroy) return;
            OnDamagedEvent?.Invoke();
            HandleDestroy();
        }
    }

}