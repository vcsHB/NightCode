using UnityEngine;

namespace Combat.SubWeaponSystem
{

    public class GrenadeBombWeapon : WeaponObjectPoolingWeapon
    {
        [SerializeField] private float _throwSpeed = 3f;
        public override void UseWeapon(SubWeaponControlData data)
        {
            data.direction.Normalize();
            float sign = Mathf.Sign(data.direction.x);
            data.direction.x = 10f * sign;
            data.direction.y = 10f;
            data.speed = _throwSpeed;
            base.UseWeapon(data);
            GrenadeBomb grenadeBomb = GetNewWeaponObject() as GrenadeBomb;
            grenadeBomb.transform.position = transform.position;

            grenadeBomb.UseWeapon(data);
        }
    }
}