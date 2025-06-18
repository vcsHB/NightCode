using System;
using System.Collections.Generic;
using Agents.Players.WeaponSystem;
using Combat.SubWeaponSystem;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponSelectionPanel : UIPanel
    {
        public Action<PlayerWeaponSO> OnWeaponSelectionEvent;
        [SerializeField] private PlayerWeaponListSO _weaponListSO;
        [SerializeField] private WeaponSelectionSlot _slotPrefab;
        [SerializeField] private List<WeaponSelectionSlot> _slotList;
        [SerializeField] private Transform _contentTrm;

        private CharacterEnum _currentCharacter;
        private WeaponDataGroup _weaponDataGroup;

        public void InitializeData(WeaponDataGroup dataGroup)
        {
            _weaponDataGroup = dataGroup;

            for (int i = 0; i < dataGroup.weaponDatas.Length; i++)
            {
                WeaponSelectionSlot slot = Instantiate(_slotPrefab, _contentTrm);
                slot.Initialize(_weaponListSO.GetWeapon(dataGroup.GetWeaponData(i).id));
                _slotList.Add(slot);
                slot.OnWeaponSelectEvent += HandleSelectWeapon;
            }
        }

        public void SelectCharacter(CharacterEnum character)
        {
            _currentCharacter = character;
            UpdateSlots();
            HandleSelectWeapon(_weaponListSO.GetWeapon(_weaponDataGroup.GetWeaponData(character).id));
        }

        private void HandleSelectWeapon(PlayerWeaponSO data)
        {
            OnWeaponSelectionEvent?.Invoke(data);
            SetWeaponData(data.id);
        }

        private void UpdateSlots()
        {
            for (int i = 0; i < _slotList.Count; i++)
            {
                if (_weaponDataGroup.weaponDatas.Length <= i) 
                {
                    _slotList[i].UnSetSelectionPanel();
                    continue;
                }
                int weaponSelectCharacter = _weaponDataGroup.weaponDatas[i].selectedCharacter;

                if (weaponSelectCharacter == -1)
                {
                    _slotList[i].UnSetSelectionPanel();
                }
                else if (weaponSelectCharacter == (int)_currentCharacter)
                {
                    _slotList[i].SetSelectionPanel();
                }
                else
                {
                    _slotList[i].SetOtherCharacterSelectionPanel();
                }
            }
        }

        private void SetWeaponData(int weaponId)
        {
            WeaponData weaponData = _weaponDataGroup.GetWeaponDataById(weaponId);

            _weaponDataGroup.GetWeaponData(_currentCharacter).selectedCharacter = weaponData.selectedCharacter;
            weaponData.selectedCharacter = (int)_currentCharacter;
            UpdateSlots();
        }
    }
}