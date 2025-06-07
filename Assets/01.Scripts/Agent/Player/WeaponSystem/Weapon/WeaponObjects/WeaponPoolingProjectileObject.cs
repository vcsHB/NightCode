using System;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class WeaponPoolingProjectileObject : MonoBehaviour
    {
        public event Action<WeaponPoolingProjectileObject> OnProjectileReturnEvent;
        protected Rigidbody2D _rigidCompo;
        protected Collider2D _collider;
        [SerializeField] private float _speed = 50f;
        protected Transform _visualTrm;
        [SerializeField] protected Caster _mainCaster;
        protected bool _isActive;
        protected Vector2 _direction;


        protected virtual void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _rigidCompo = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public virtual void Fire(Vector2 direction)
        {
            direction.Normalize();
            _visualTrm.right = direction;
            _direction = direction;
            _rigidCompo.linearVelocity = direction * _speed;

        }



        protected virtual void ReturnToPool()
        {
            OnProjectileReturnEvent?.Invoke(this);
        }

        public virtual void ResetProjectile()
        {

        }
    }
}