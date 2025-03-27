using System;
using UnityEngine;
namespace Combat.SubWeaponSystem
{

    public abstract class WeaponObject : MonoBehaviour
    {
        public abstract void UseWeapon(SubWeaponControlData data);
        
    }
}