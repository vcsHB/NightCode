using Chipset;
using Core.DataControl;
using System;
using System.Collections.Generic;
using System.IO;
using UI.GameSelectScene;
using UnityEngine;

namespace Map
{
    public class CharacterDataController : MonoBehaviour
    {
        private JsonLoadHelper<CharacterSave> _loadHelper =
            new JsonLoadHelper<CharacterSave>(Path.Combine(Application.dataPath, "Save/CharacterData.json"));

        [SerializeField] private CharacterDataInitializeSO _initializeData;

        [Space]
        [SerializeField] private MapController _mapController;
        [SerializeField] private CharacterBuildController _buildController;

        //이걸 넘기고 다른 스크립트에서 이걸 가지고 뭘 하게 하는 느낌일거임
        private CharacterSave _characterSave;

        private void Awake()
        {
            Load();
            _buildController.Initialize(_characterSave);
            _mapController.Initialize(_characterSave.characterData.ConvertAll(data => data.characterPosition));
        }

        public void Save()
        {
            _loadHelper.Save(_characterSave);
        }

        public void Load()
        {
            _characterSave = _loadHelper.Load();
            if (_characterSave.containWeaponList.Count <= 0) InitializeData();
        }

        private void InitializeData()
        {
            _characterSave.openInventory = _initializeData.openInventory;
            _initializeData.containChipsets.ForEach(chipset => _characterSave.containChipsetList.Add(chipset.id));

            _characterSave.characterData[(int)CharacterEnum.An].equipWeaponId = _initializeData.anInitializeWeapon.id;
            _characterSave.characterData[(int)CharacterEnum.JinLay].equipWeaponId = _initializeData.jinInitializeWeapon.id;
            _characterSave.characterData[(int)CharacterEnum.Bina].equipWeaponId = _initializeData.binaInitializeWeapon.id;

            for (int i = 0; i < 3; i++)
            {
                _characterSave.characterData[i].playerHealth = _initializeData.characterDefaultHealth;
                _characterSave.containWeaponList.Add(_characterSave.characterData[i].equipWeaponId);
            }
        }
    }
}
