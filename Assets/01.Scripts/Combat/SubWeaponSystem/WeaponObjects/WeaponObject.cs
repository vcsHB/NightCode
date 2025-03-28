using UnityEngine;
namespace Combat.SubWeaponSystem
{
    /// <summary>
    /// basic Weapon Object Class
    /// manage relationship order
    /// WeaponController -> Weapon -> [WeaponObject]
    /// </summary>
    public abstract class WeaponObject : MonoBehaviour
    {
        /// <summary>
        /// Call in weapon use
        /// </summary>
        /// <param name="data"></param>
        public abstract void UseWeapon(SubWeaponControlData data);
        
    }
}