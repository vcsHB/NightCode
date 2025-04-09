using UnityEngine;
namespace Combat.SubWeaponSystem
{
    [System.Serializable]
    public struct WeaponLevelStatusData
    {
        public int amount;
        public float damage;
        public int cost;
    }
    [CreateAssetMenu(menuName = "SO/Combat/SubWeaponSO")]
    public class SubWeaponSO : ScriptableObject
    {
        public int id;
        public SubWeaponType type;
        public string weaponName;
        public string description;
        public Sprite subWeaponSprite;
        public SubWeapon subWeaponPrefab;
        [Header("Reinforce Setting")]
        public WeaponLevelStatusData[] levelUpData;


    }
}