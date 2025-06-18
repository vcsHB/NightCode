using Agents.Players.WeaponSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UI.NodeViewScene.WeaponSelectionUIs;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Core.DataControl
{
    public class CharacterDataLoader : MonoBehaviour
    {
        [SerializeField] private CharacterDataInitializeSO _initializeData;

        private string _path = Path.Combine(Application.dataPath, "Save/CharacterData.json");
        private WeaponMainPanel _weaponMainPanel;

        private CharacterSave _characterSave;
        [SerializeField] private WeaponDataGroup _weaponDataGroup;

        public void Init()
        {
            Load();
            _weaponMainPanel = GetComponentInChildren<WeaponMainPanel>();

            _weaponDataGroup = new WeaponDataGroup();
            _weaponDataGroup.weaponDatas = new WeaponData[_characterSave.containWeaponId.Count];

            for (int i = 0; i < _characterSave.containWeaponId.Count; i++)
            {
                var characterData = _characterSave.charcterData.Find(data => data.weaponId == _characterSave.containWeaponId[i]);
                int selected = _characterSave.charcterData.IndexOf(characterData);
                WeaponData weaponData = new WeaponData(_characterSave.containWeaponId[i], selected);
                _weaponDataGroup.weaponDatas[i] = weaponData;
            }

            _weaponMainPanel.InitializeData(_weaponDataGroup);
        }


        public void ChangeWeapon(CharacterEnum character, int weaponId)
        {
            _characterSave.charcterData[(int)character].weaponId = weaponId;
        }

        public void SaveWeaponData()
        {
            _characterSave.containWeaponId = _weaponDataGroup.weaponDatas.ToList().ConvertAll(data => data.id);

            for (int i = 0; i < 3; i++)
            {
                _characterSave.charcterData[i].weaponId = _weaponDataGroup.GetWeaponData(i).id;
            }

        }

        public void Save()
        {
            if (_characterSave == null) InitializeData();

            string json = JsonUtility.ToJson(_characterSave, true);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                InitializeData();
                Save();
                return;
            }

            string json = File.ReadAllText(_path);
            _characterSave = JsonUtility.FromJson<CharacterSave>(json);
        }

        public void InitializeData()
        {
            _characterSave = new CharacterSave();

            _characterSave.charcterData[(int)CharacterEnum.An].weaponId = _initializeData.anInitializeWeapon.id;
            _characterSave.charcterData[(int)CharacterEnum.JinLay].weaponId = _initializeData.jinInitializeWeapon.id;
            _characterSave.charcterData[(int)CharacterEnum.Bina].weaponId = _initializeData.binaInitializeWeapon.id;

            for (int i = 0; i < 3; i++)
            {
                _characterSave.charcterData[i].health = 100;
                _characterSave.containWeaponId.Add(_characterSave.charcterData[i].weaponId);
            }
        }

        public void PlayerDead(CharacterEnum character)
        {
            if (_characterSave == null) Load();
            if (_characterSave.containWeaponId.Contains(_characterSave.charcterData[(int)character].weaponId))
            {
                _characterSave.containWeaponId.Remove(_characterSave.charcterData[(int)character].weaponId);
            }
            string json = JsonUtility.ToJson(_characterSave, true);
            File.WriteAllText(_path, json);
        }
    }

    [Serializable]
    public class CharacterSave
    {
        public List<CharacterData> charcterData;
        public List<int> containWeaponId;
        public int credit;

        public CharacterSave()
        {
            containWeaponId = new List<int>();
            charcterData = new List<CharacterData>();

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                charcterData.Add(new CharacterData());
            }
        }
    }

    [Serializable]
    public class CharacterData
    {
        public int weaponId;
        public int health;
    }
}
