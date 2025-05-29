using System;
using Agents.Players.WeaponSystem;
using UnityEngine;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponMainPanel : MonoBehaviour
    {
        [SerializeField] private WeaponDetailPanel _weaponDetailPanel;
        [SerializeField] private WeaponInfoPanel _weaponInfoPanel;
        [SerializeField] private WeaponDataGroup _debugData;
        private void Awake()
        {
            _weaponInfoPanel.OnWeaponSelectEvent += HandleWeaponSelect;

            // Debug Code
            InitializeData(_debugData);
        }

        private void HandleWeaponSelect(PlayerWeaponSO data)
        {
            _weaponDetailPanel.SetWeaponData(data);
        }

        public void InitializeData(WeaponDataGroup dataGroup)
        {
            _weaponInfoPanel.InitializeData(dataGroup);
        }

    }
}