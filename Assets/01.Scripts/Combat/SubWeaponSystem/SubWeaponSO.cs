using System.Data.Common;
using UnityEngine;
namespace Combat.SubWeaponSystem
{
    [System.Serializable]
    public struct WeaponLevelStatusData
    {
        public WeaponStatusIncreaseData[] increaseData; // increased Value
        public int cost;
    }

    [System.Serializable]
    public struct WeaponStatusIncreaseData
    {
        public WeaponStatusSO statusType;
        public float value;
    }
    [CreateAssetMenu(menuName = "SO/Combat/SubWeapons/SubWeaponSO")]
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

        public WeaponStatusIncreaseData[] GetIncreaseData(int level)
        {
            level--; // Level -> index
            if (level < 0 || levelUpData.Length < level + 1)
            {
                Debug.LogError($"Can't Get Weapon Status Data (over range index) index:{level}");
                return null;
            }
            return levelUpData[level].increaseData;


        }
        public bool IsMaxLevel(int level) => levelUpData.Length <= level;

    }
}