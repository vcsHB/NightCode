using Combat.SubWeaponSystem;
using UI.OfficeScene.Armory;
using UnityEngine;

namespace Office.Armory
{

    public class ArmoryPanel : MonoBehaviour
    {
        private WeaponSlot[] _slots;
        [SerializeField] private WeaponInfoPanel _weaponInfoPanel;
        [SerializeField] private SubWeaponData _debugData;
        private void Awake()
        {
            _slots = GetComponentsInChildren<WeaponSlot>();
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnSelectEvent += HandleSlotSelected;
            }
        }

        private void HandleSlotSelected(SubWeaponSO weaponSO)
        {
            if (weaponSO == null) return;
            _weaponInfoPanel.SetWeaponData(weaponSO, _debugData);
            _weaponInfoPanel.Open();
        }
    }

}
