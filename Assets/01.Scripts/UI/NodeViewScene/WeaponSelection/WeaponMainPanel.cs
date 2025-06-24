using System;
using Agents.Players.WeaponSystem;
using UnityEngine;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponMainPanel : MonoBehaviour
    {
        [SerializeField] private WeaponDetailPanel _weaponDetailPanel;
        [SerializeField] private WeaponInfoPanel _weaponInfoPanel;

        private CharacterEnum _selectedCharacter;

        private void Awake()
        {
            _weaponInfoPanel.OnWeaponSelectEvent += HandleWeaponSelect;
        }

        private void HandleWeaponSelect(PlayerWeaponSO data)
        {
            _weaponDetailPanel.SetWeaponData(data);
        }

        public void InitializeData(WeaponDataGroup dataGroup)
        {
            _weaponInfoPanel.InitializeData(dataGroup);
        }

        public void SelectCharacter(CharacterEnum characterEnum)
        {
            _selectedCharacter = characterEnum;
            _weaponInfoPanel.SelectCharacter(_selectedCharacter);
        }
    }
}