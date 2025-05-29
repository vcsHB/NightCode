using Agents.Players.SkillSystem;
using Core.Attribute;
using UnityEngine;
namespace Agents.Players.WeaponSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/WeaponSO")]
    public class PlayerWeaponSO : ScriptableObject
    {
        [ReadOnly]
        public int id;
        public string weaponName;
        [TextArea]
        public string weaponDescription;
        public Sprite weaponIcon;
        [Header("Normal Skill")]
        public int normalSkillCostEnergy;
        public string normalSkillName;
        [TextArea]
        public string normalSkillDescription;
        public PlayerWeapon weaponPrefab;
        [Header("Active Skill")]
        public PlayerSkillSO skillSO;

        [Header("DisplaySetting")]
        public CombatMethodTagSO[] combatMethods;



        public void SetId(int newId)
        {
            id = newId;
        }
    }
}