using Agents.Players.SkillSystem;
using UnityEngine;
namespace Agents.Players.WeaponSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/WeaponSO")]
    public class PlayerWeaponSO : ScriptableObject
    {
        public int id;
        public string weaponName;
        public string weaponDescription;
        public Sprite weaponIcon;
        public int skillCostEnergy;
        public PlayerWeapon weaponPrefab;
        public PlayerSkillSO skillSO;

        [Header("DisplaySetting")]
        public CombatMethodTagSO[] combatMethods;



        public void SetId(int newId)
        {
            id = newId;
        }
    }
}