using Agents.Players.WeaponSystem;
using UnityEngine;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponDetailPanel : MonoBehaviour
    {
        [SerializeField] private WeaponNormalSkillInfoPanel _normalSkillPanel;
        [SerializeField] private WeaponActiveSkillInfoPanel _activeSkillPanel;
        [SerializeField] private CombatMethodPanel _combatMethodPanel;

        public void SetWeaponData(PlayerWeaponSO data)
        {
            _normalSkillPanel.SetWeaponNormalSkillData(data);
            _activeSkillPanel.SetWeaponActiveSkillData(data.skillSO);
            _combatMethodPanel.SetCombatMethodTagData(data.combatMethods);
        }
    }

}