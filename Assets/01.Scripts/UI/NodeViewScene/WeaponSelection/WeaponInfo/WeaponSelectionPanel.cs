using System;
using System.Collections.Generic;
using Agents.Players.WeaponSystem;
using UnityEngine;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponSelectionPanel : UIPanel
    {
        public Action<PlayerWeaponSO> OnWeaponSelectionEvent;
        [SerializeField] private PlayerWeaponListSO _weaponListSO;
        [SerializeField] private WeaponSelectionSlot _slotPrefab;
        [SerializeField] private List<WeaponSelectionSlot> _slotList;
        [SerializeField] private Transform _contentTrm;

        protected override void Awake()
        {
            base.Awake();

        }

        public void InitializeData(WeaponDataGroup dataGroup)
        {
            for (int i = 0; i < dataGroup.weaponDatas.Length; i++)
            {
                if (dataGroup.GetWeaponData(i).isEnabled)
                {
                    WeaponSelectionSlot slot = Instantiate(_slotPrefab, _contentTrm);
                    slot.Initialize(_weaponListSO.GetWeapon(dataGroup.GetWeaponData(i).id));
                    _slotList.Add(slot);
                    slot.OnWeaponSelectEvent += HandleSelectWeapon;
                }

            }
        }

        private void HandleSelectWeapon(PlayerWeaponSO data)
        {
            OnWeaponSelectionEvent?.Invoke(data);
        }
    }
}