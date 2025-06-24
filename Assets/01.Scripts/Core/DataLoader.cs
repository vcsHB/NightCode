using Agents.Players.WeaponSystem;
using Chipset;
using Map;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core.DataControl
{
    public class DataLoader : MonoSingleton<DataLoader>
    {
        public UnityEvent onLoad;
        public CharacterDataInitializeSO characterInitialize;
        public ChipsetInitializeSO chipsetInitialize;

        public MapGraphSO mapGraph;
        public ChipsetGroupSO chipsetGroup;
        public PlayerWeaponListSO weaponList;
        private static string _mapSavePath = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private static string _characterSavePath = Path.Combine(Application.dataPath, "Save/CharacterData.json");
        private static string _userDataSavePath = Path.Combine(Application.dataPath, "Save/UserData.json");

        private MapSave _mapSave;
        private CharacterSave _characterSave;
        private UserData _userData;

        public int Credit => _characterSave.credit;
        
        public void GoToMenu()
        {
            SceneManager.LoadScene(SceneName.TitleScene);
        }

        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        public void AddCredit(int amount)
        {
            _characterSave.credit += amount;
            Save();
        }

        public MapNodeSO GetCurrentMap()
        {
            if (_mapSave == null) Load();
            return mapGraph.GetNodeSO(_mapSave.enterStageId);
        }

        public List<CharacterEnum> GetCharacters()
        {
            List<CharacterEnum> characters = new List<CharacterEnum>();

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_characterSave.characterData[(int)character].characterPosition == _mapSave.enterStagePosition)
                {
                    characters.Add(character);
                }
            }
            return characters;
        }

        public PlayerWeaponSO GetWeapon(CharacterEnum character)
        {
            return weaponList.GetWeapon(_characterSave.characterData[(int)character].equipWeaponId);
        }

        public UserData GetUserData()
        {
            return _userData;
        }

        public ChipsetSO[] GetChipset(CharacterEnum character)
        {
            if (_characterSave == null) Load();
            return _characterSave.GetCharacterChipset(character).
                ConvertAll(save =>
                {
                    ushort chipsetId = _characterSave.containChipsetList[save.chipsetIndex];
                    return chipsetGroup.GetChipset(chipsetId);
                }).ToArray();
        }

        public void CompleteMap()
        {
            _characterSave.clearEnteredStage = true;
            _characterSave.failEnteredStage = false;
            Save();
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }

        public void FailMap()
        {
            _characterSave.clearEnteredStage = false;
            _characterSave.failEnteredStage = true;
            Save();
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }

        public void Load()
        {
            onLoad?.Invoke();

            if (File.Exists(_mapSavePath) == false)
            {
                _mapSave = new MapSave();

                string json = JsonUtility.ToJson(_mapSave);
                File.WriteAllText(_mapSavePath, json);
            }
            else
            {
                string mapJson = File.ReadAllText(_mapSavePath);
                _mapSave = JsonUtility.FromJson<MapSave>(mapJson);
            }

            if (File.Exists(_characterSavePath) == false)
            {
                _characterSave = new CharacterSave();

                string json = JsonUtility.ToJson(_characterSave);
                File.WriteAllText(_characterSavePath, json);
            }
            else
            {
                string characterJson = File.ReadAllText(_characterSavePath);
                _characterSave = JsonUtility.FromJson<CharacterSave>(characterJson);
            }

            if (File.Exists(_userDataSavePath) == false)
            {
                _userData = new UserData();

                string json = JsonUtility.ToJson(_userData);
                File.WriteAllText(_userDataSavePath, json);
            }
            else
            {
                string userDataJson = File.ReadAllText(_userDataSavePath);
                _userData = JsonUtility.FromJson<UserData>(userDataJson);
            }

            //Dictionary<string, Action<string>> loadActions = new()
            //{
            //    { _mapSavePath,         json => _mapSave = JsonUtility.FromJson<MapSave>(json) ?? null },
            //    { _chipsetSavePath,     json => _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(json) ?? null },
            //    { _characterSavePath,   json => _characterSave = JsonUtility.FromJson<CharacterSave>(json) ?? null },
            //    { _userDataSavePath,    json => _userData = JsonUtility.FromJson<UserData>(json) ?? new UserData() },
            //};

            //foreach (var entry in loadActions)
            //{
            //    string path = entry.Key;

            //    if (!File.Exists(path))
            //    {
            //        File.WriteAllText(path, "{}");
            //    }

            //    string json = File.ReadAllText(path);
            //    entry.Value(json);
            //}
        }

        public void Save()
        {
            string mapJson = JsonUtility.ToJson(_mapSave);
            string characterJson = JsonUtility.ToJson(_characterSave);
            string userJson = JsonUtility.ToJson(_userData);

            File.WriteAllText(_mapSavePath, mapJson);
            File.WriteAllText(_characterSavePath, characterJson);
            File.WriteAllText(_userDataSavePath, userJson);
        }

        public void AddChipset(ushort id)
        {
            _characterSave.containChipsetList.Add(id);
            Save();
        }

        public void AddWeapon(int id)
        {
            _characterSave.containWeaponList.Add(id);
            Save();
        }

        public bool IsWeaponExist(int id)
            => _characterSave.containWeaponList.Contains(id);
    }
}
