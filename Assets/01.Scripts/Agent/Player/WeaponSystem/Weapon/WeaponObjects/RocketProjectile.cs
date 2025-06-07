using System;
using Combat;
using Combat.Casters;
using ObjectManage;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class RocketProjectile : MonoBehaviour, IDamageable
    {
        public UnityEvent OnRocketDestroyEvent;
        [SerializeField] private float _flySpeed = 50f;
        public event Action<RocketProjectile> OnRocketReturnEvent;
        private Rigidbody2D _rigidCompo;
        [SerializeField] private PoolingType _destroyExplosionVFX;
        [SerializeField] private ParticleSystem _rocketBoostVFX;
        private Caster _caster;
        private Collider2D _collider;
        private Transform _visualTrm;
        private bool _isActive;

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _collider = GetComponent<Collider2D>();
            _rigidCompo = GetComponent<Rigidbody2D>();
            _caster = GetComponentInChildren<Caster>();
            _caster.OnCastSuccessEvent.AddListener(DestroyRocket);
        }

        public void Fire(Vector2 direction)
        {
            _rocketBoostVFX.Play();
            _visualTrm.right = direction;
            _isActive = true;
            _rigidCompo.linearVelocity = direction.normalized * _flySpeed;
        }
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void Update()
        {
            if (_isActive)
            {
                _caster.Cast();
            }
        }

        private void DestroyRocket()
        {
            _rocketBoostVFX.Stop();
            _collider.enabled = false;
            VFXPlayer vfx = PoolManager.Instance.Pop(_destroyExplosionVFX, transform.position, Quaternion.identity) as VFXPlayer;
            vfx.Play();

            _isActive = false;
            _rigidCompo.linearVelocity = Vector2.zero;
            OnRocketReturnEvent?.Invoke(this);
            OnRocketDestroyEvent?.Invoke();
        }

        public bool ApplyDamage(CombatData data)
        {
            if (data.damage > 0)
            {
                DestroyRocket();
                return true;
            }
            return false;
        }
    }
}