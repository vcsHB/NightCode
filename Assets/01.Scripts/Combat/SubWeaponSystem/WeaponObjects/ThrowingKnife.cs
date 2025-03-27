using SubWeaponSystem;
using UnityEngine;
namespace Combat.SubWeaponSystem.WepaonObjects
{

    public class ThrowingKnife : ProjectileSubWeaponObject
    {
        private Transform _visualTrm;

        protected override void Awake()
        {
            base.Awake();
            _visualTrm = transform.Find("Visual");
        }

        public override void UseWeapon(SubWeaponControlData data)
        {
            _visualTrm.right = data.direction;
            
        }

    }
}