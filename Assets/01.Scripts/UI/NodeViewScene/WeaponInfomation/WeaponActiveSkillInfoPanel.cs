using Agents.Players.SkillSystem;
namespace UI.NodeViewScene
{

    public class WeaponActiveSkillInfoPanel : WeaponAbilityInfoPanel
    {
        public void SetWeaponNormalSkillData(PlayerSkillSO weaponData)
        {
            _titleText.text = weaponData.skillName;
            _descriptionText.text = weaponData.skillDescription;
            _requireCostText.text = weaponData.skillCostEnergy.ToString(); 
        }
    }
}