using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Core.DataControl
{
    public class CharacterDataLoader : MonoBehaviour
    {
        [SerializeField] private CharacterDataInitializeSO _initializeData;

        private string _path = Path.Combine(Application.dataPath, "Save/CharacterData.json");
        private CharacterSave _characterSave;

        public void Initialize()
        {
            _characterSave = new CharacterSave();

            _characterSave.charcterData[(int)CharacterEnum.An].weaponId = _initializeData.anInitializeWeapon.id;
            _characterSave.charcterData[(int)CharacterEnum.JinLay].weaponId = _initializeData.jinInitializeWeapon.id;
            _characterSave.charcterData[(int)CharacterEnum.Bina].weaponId = _initializeData.binaInitializeWeapon.id;

            for(int i = 0; i < 3; i++)
            {
                _characterSave.charcterData[i].health = 100;
                _characterSave.containWeaponId.Add(_characterSave.charcterData[i].weaponId);
            }

        }

        public void Save()
        {
            if (_characterSave == null) Initialize();

            string json = JsonUtility.ToJson(_characterSave, true);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if(File.Exists(_path) == false)
            {
                Initialize();
                return;
            }

            string json = File.ReadAllText(_path);
            _characterSave = JsonUtility.FromJson<CharacterSave>(json);
        }
    }

    [Serializable]
    public class CharacterSave
    {
        public List<CharacterData> charcterData;
        public List<int> containWeaponId;

        public CharacterSave()
        {
            containWeaponId = new List<int>();
            charcterData = new List<CharacterData>();

            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
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
