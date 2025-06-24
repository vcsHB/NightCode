using Agents.Players.WeaponSystem;
using Chipset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UI.NodeViewScene.WeaponSelectionUIs;
using UnityEngine;

namespace Core.DataControl
{
    public class CharacterDataLoader : MonoBehaviour
    {
        private WeaponMainPanel _weaponMainPanel;

        private CharacterSave _characterSave;
        private WeaponDataGroup _weaponDataGroup;

        public void Init()
        {
            _weaponMainPanel = GetComponentInChildren<WeaponMainPanel>();

            _weaponDataGroup = new WeaponDataGroup();
            _weaponDataGroup.weaponDatas = new WeaponData[_characterSave.containWeaponList.Count];

            for (int i = 0; i < _characterSave.containWeaponList.Count; i++)
            {
                var characterData = _characterSave.characterData.Find(data => data.equipWeaponId == _characterSave.containWeaponList[i]);
                int selected = _characterSave.characterData.IndexOf(characterData);
                WeaponData weaponData = new WeaponData(_characterSave.containWeaponList[i], selected);
                _weaponDataGroup.weaponDatas[i] = weaponData;
            }

            _weaponMainPanel.InitializeData(_weaponDataGroup);
        }
    }
}
