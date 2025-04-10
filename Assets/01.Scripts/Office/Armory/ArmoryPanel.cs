using System;
using Combat.SubWeaponSystem;
using UI.OfficeScene.Armory;
using UnityEngine;

namespace Office.Armory
{

    public class ArmoryPanel : MonoBehaviour
    {
        private WeaponSlot[] _slots;
        [SerializeField] private WeaponInventroyController _weaponInventory;
        [SerializeField] private CreditStorage _creditStorage;
        [SerializeField] private PurchasePopUpPanel _purchasePanel;
        [SerializeField] private WeaponInfoPanel _weaponInfoPanel;
        [SerializeField] private SubWeaponData _debugData;
        private SubWeaponSO _currentSelectedWeaponSO;
        private WeaponSlot _currentSelectedSlot;

        private void Awake()
        {
            _purchasePanel.OnPurchaseEvent += HandlePurchaseWeapon;
            _slots = GetComponentsInChildren<WeaponSlot>();

        }

        private void Start()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnSelectEvent += HandleSlotSelected;
                if (_weaponInventory.IsUnlocked(_slots[i].WeaponSO.id))
                {
                    _slots[i].SetActive(true);
                }
                else
                    _slots[i].SetActive(false);
            }
        }
        private void OnDestroy()
        {
            _purchasePanel.OnPurchaseEvent -= HandlePurchaseWeapon;
        }

        private void HandlePurchaseWeapon()
        {
            if (_weaponInventory.IsUnlocked(_currentSelectedWeaponSO.id)) return;
            if (!_creditStorage.UseCredit(_currentSelectedWeaponSO.initialPrice)) return;

            _weaponInventory.UnlockWeapon(_currentSelectedWeaponSO.id);
            _currentSelectedSlot.SetActive(true);
            HandleSlotSelected(_currentSelectedWeaponSO, null); // debug
        }

        private void HandleSlotSelected(SubWeaponSO weaponSO, WeaponSlot slot)
        {
            if (weaponSO == null) return;
            _currentSelectedWeaponSO = weaponSO;
            _currentSelectedSlot = slot;
            if (_weaponInventory.IsUnlocked(weaponSO.id))
            {
                _weaponInfoPanel.SetWeaponData(weaponSO, _debugData);
                _weaponInfoPanel.Open();
                _purchasePanel.Close();
            }
            else
            {
                _weaponInfoPanel.Close();
                _purchasePanel.Open();
                _purchasePanel.SetPurchaseData(weaponSO, _creditStorage.CurrentCreditAmount, slot.transform.position);
            }
        }
    }

}
