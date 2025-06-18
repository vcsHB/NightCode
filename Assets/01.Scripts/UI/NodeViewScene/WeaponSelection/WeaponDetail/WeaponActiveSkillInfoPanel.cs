using Agents.Players.SkillSystem;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponActiveSkillInfoPanel : WeaponAbilityInfoPanel
    {
        public void SetWeaponActiveSkillData(PlayerSkillSO weaponData)
        {
            _titleText.text = weaponData.skillName;
            _descriptionText.text = weaponData.skillDescription;
            _requireCostText.text = weaponData.skillCostEnergy.ToString(); 
        }
    }
}