using UnityEngine;
namespace Combat.SubWeaponSystem
{
    [CreateAssetMenu(menuName ="SO/Combat/SubWeaponSO")]
    public class SubWeaponSO :ScriptableObject
    {
        public int id;
        public SubWeaponType type;
        public string weaponName;
        public string description;
        public Sprite subWeaponSprite;
        public SubWeapon subWeaponPrefab;
        

    }
}