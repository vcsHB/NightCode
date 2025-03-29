using UnityEngine;
namespace Combat.SubWeaponSystem
{
    public class ThrowingWepaonObject : PoolableWeaponObject
    {
        protected Rigidbody2D _rigid;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        public override void UseWeapon(SubWeaponControlData data)
        {
            _rigid.linearVelocity = data.direction * data.speed;
        }

    }
}