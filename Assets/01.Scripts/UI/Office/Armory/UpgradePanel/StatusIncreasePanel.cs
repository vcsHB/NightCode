using System.Collections.Generic;
using Combat.SubWeaponSystem;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public class StatusIncreasePanel : WeaponInformationPanel
    {
        [SerializeField] private WeaponStatusSlot _weaponStatusSlotPrefab;
        [SerializeField] private Transform _contentTrm;
        private Queue<WeaponStatusSlot> _slotPool = new();
        private Queue<WeaponStatusSlot> _enabledSlots = new();

        public override void SetWeaponData(SubWeaponSO weapon, SubWeaponData weaponData)
        {
            DisableAllSlots();
            WeaponStatusIncreaseData[] datas = weapon.GetIncreaseData(weaponData.level);
            bool isMaxLevel = weapon.IsMaxLevel(weaponData.level);
            for (int i = 0; i < datas.Length; i++)
            {
                WeaponStatusSlot slot = GetWeaponStatusSlot();
                if (isMaxLevel)
                    slot.SetMaxStatus(datas[i].value, datas[i].statusType);
                else
                    slot.SetStatusIncrease(datas[i].value, datas[i + 1].value, datas[i].statusType);
            }
        }

        private void DisableAllSlots()
        {
            for (int i = 0; i < _enabledSlots.Count; i++)
            {
                WeaponStatusSlot slot = _enabledSlots.Dequeue();
                slot.SetActive(false);
                _slotPool.Enqueue(slot);
            }
        }

        private WeaponStatusSlot GetWeaponStatusSlot()
        {
            WeaponStatusSlot newSlot = _slotPool.Count <= 0 ?
                Instantiate(_weaponStatusSlotPrefab, _contentTrm) :
                _slotPool.Dequeue();
            newSlot.SetActive(true);
            return newSlot;
        }
    }
}