using System.Collections.Generic;
using Combat.SubWeaponSystem;
using UnityEngine;
namespace UI.InGame.GameUI.Combat.SubWeaponSystem
{
    public class SubWeaponPanel : MonoBehaviour
    {
        private WeaponDetailPanel[] _weaponDetailPanels;
        private Dictionary<SubWeaponType, WeaponDetailPanel> _weaponDetailPanelDictionary = new();

        private void Awake()
        {
            _weaponDetailPanels = GetComponentsInChildren<WeaponDetailPanel>();
            foreach (var panel in _weaponDetailPanels)
            {
                if (!_weaponDetailPanelDictionary.ContainsKey(panel.SubWeaponType))
                {
                    _weaponDetailPanelDictionary.Add(panel.SubWeaponType, panel);
                }
                else
                    Debug.LogWarning($"Already Exist Panel Type! : {panel.SubWeaponType}");
            }

        }

        public void SetWeapon(SubWeaponSO data)
        {
            DisableAll();
            WeaponDetailPanel weaponData = GetDetailPanel(data.type);
            if (weaponData == null) return;
            weaponData.SetData(data);
            SetEnableDetail(data.type);

        }
        private WeaponDetailPanel GetDetailPanel(SubWeaponType type)
        {
            if (_weaponDetailPanelDictionary.TryGetValue(type, out WeaponDetailPanel panel))
                return panel;

            Debug.LogWarning($"Detail Panel Type[{type}] is not exist.");
            return null;
        }

        public void SetEnableDetail(SubWeaponType type)
        {
            WeaponDetailPanel panel = GetDetailPanel(type);
            if (panel == null) return;
            panel.Open();
        }

        private void DisableAll()
        {
            foreach (var panel in _weaponDetailPanels)
            {
                panel.Close();
            }
        }
    }
}