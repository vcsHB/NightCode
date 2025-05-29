using Agents.Players.WeaponSystem;
using UnityEngine;
namespace UI.NodeViewScene
{

    public class WeaponNormalSkillInfoPanel : WeaponAbilityInfoPanel
    {
        public void SetWeaponNormalSkillData(PlayerWeaponSO weaponData)
        {
            _titleText.text = weaponData.normalSkillName;
            _descriptionText.text = weaponData.normalSkillDescription;
            _requireCostText.text = weaponData.normalSkillCostEnergy.ToString(); 
        }
    }
}