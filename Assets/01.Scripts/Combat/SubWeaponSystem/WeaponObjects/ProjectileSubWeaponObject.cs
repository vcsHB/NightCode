using Combat.Casters;
using Combat.SubWeaponSystem;
using UnityEngine;
namespace SubWeaponSystem
{

    public abstract class ProjectileSubWeaponObject : PoolableWeaponObject
    {
        [SerializeField] protected Caster _caster;
        protected Rigidbody2D _rigid;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        public void SetVelocity(Vector2 newVelocity)
        {
            _rigid.linearVelocity = newVelocity;
        }
        protected void StopImmediately()
        {
            _rigid.linearVelocity = Vector2.zero;
        }

    }
}