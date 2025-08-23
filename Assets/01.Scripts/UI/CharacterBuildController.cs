using Agents.Players.WeaponSystem;
using Chipset;
using Core.DataControl;
using System.Collections.Generic;
using System;
using UI.NodeViewScene.WeaponSelectionUIs;
using UnityEngine;
using UI.GameSelectScene.CharacterSetting;

namespace UI.GameSelectScene
{
    public class CharacterBuildController : MonoBehaviour
    {
        [SerializeField] private ChipsetGroupSO _chipsetGroupSO;
        private WeaponMainPanel _mainPanel;
        private AgentSelectController _agentSelectController;

        private WeaponDataGroup _weaponDataGroup;

        public void Initialize(CharacterSave save)
        {
            _mainPanel = GetComponentInChildren<WeaponMainPanel>();
            _agentSelectController = GetComponentInChildren<AgentSelectController>();

            InitializeWeaponData(save);
            _mainPanel.InitializeData(_weaponDataGroup);
            _agentSelectController.Initialize();

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (save.characterData[(int)character].isPlayerDead)
                {
                    _agentSelectController.RetireCharacter(character);
                    continue;
                }
            }
        }

        private void InitializeWeaponData(CharacterSave save)
        {
            _weaponDataGroup = new WeaponDataGroup();
            _weaponDataGroup.weaponDatas = new WeaponData[save.containWeaponList.Count];

            for (int i = 0; i < save.containWeaponList.Count; i++)
            {
                var characterData = save.characterData.Find(data => data.equipWeaponId == save.containWeaponList[i] && !data.isPlayerDead);
                int selected = save.characterData.IndexOf(characterData);
                WeaponData weaponData = new WeaponData(save.containWeaponList[i], selected);
                _weaponDataGroup.weaponDatas[i] = weaponData;
            }
        }

        public int GetCharacterWeapon(CharacterEnum character)
        {
            for (int i = 0; i < _weaponDataGroup.weaponDatas.Length; i++)
            {
                if (_weaponDataGroup.weaponDatas[i].selectedCharacter == (int)character)
                {
                    return _weaponDataGroup.weaponDatas[i].id;
                }
            }
            return -1;
        }
    }
}
