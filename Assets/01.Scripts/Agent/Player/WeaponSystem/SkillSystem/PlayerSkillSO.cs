using UnityEngine;
namespace Agents.Players.SkillSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/Skills/PlayerSkillSO")]
    public class PlayerSkillSO : ScriptableObject
    {
        public string skillName;
        [TextArea]
        public string skillDescription;
        public Sprite skillIcon;
        public int skillCostEnergy;
        public float skillCooltime = 30f;
        
        public PlayerSkill skillPrefab;

    }
}