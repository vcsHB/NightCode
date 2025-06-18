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
        public ChipsetGruopSO chipsetGroup;
        public PlayerWeaponListSO weaponList;
        private static string _mapSavePath = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private static string _chipsetSavePath = Path.Combine(Application.dataPath, "Save/Chipset.json");
        private static string _characterSavePath = Path.Combine(Application.dataPath, "Save/CharacterData.json");
        private static string _userDataSavePath = Path.Combine(Application.dataPath, "Save/UserData.json");

        private MapSave _mapSave;
        private ChipsetInventorySave _chipsetSave;
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
                if (_mapSave.characterPositions[(int)character] == _mapSave.enterStagePosition)
                {
                    characters.Add(character);
                }
            }
            return characters;
        }

        public PlayerWeaponSO GetWeapon(CharacterEnum character)
        {
            return weaponList.GetWeapon(_characterSave.charcterData[(int)character].weaponId);
        }

        public UserData GetUserData()
        {
            return _userData;
        }

        public ChipsetSO[] GetChipset(CharacterEnum character)
        {
            if (_chipsetSave == null) Load();
            return _chipsetSave.GetChipsets(character).
                ConvertAll(save =>
                {
                    ushort chipsetId = _chipsetSave.containChipsets[save.chipsetindex];
                    return chipsetGroup.GetChipset(chipsetId);
                }).ToArray();
        }

        public void CompleteMap()
        {
            _mapSave.isEnteredStageClear = true;
            _mapSave.isFailStageClear = false;
            Save();
        }

        public void FailMap()
        {
            _mapSave.isEnteredStageClear = false;
            _mapSave.isFailStageClear = true;
            Save();
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

            if (File.Exists(_chipsetSavePath) == false)
            {
                _chipsetSave = new ChipsetInventorySave();

                _chipsetSave.openInventory = chipsetInitialize.openInventory;
                _chipsetSave.containChipsets = chipsetInitialize.containChipsets.ConvertAll(Chipset => Chipset.id);

                _chipsetSave.anChipset = new List<ChipsetSave>();
                _chipsetSave.jinLayChipset = new List<ChipsetSave>();
                _chipsetSave.binaChipset = new List<ChipsetSave>();

                string json = JsonUtility.ToJson(_chipsetSave);
                File.WriteAllText(_chipsetSavePath, json);
            }
            else
            {
                string chipsetJson = File.ReadAllText(_chipsetSavePath);
                _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(chipsetJson);
            }

            if (File.Exists(_characterSavePath) == false)
            {
                _characterSave = new CharacterSave();

                _characterSave.charcterData[(int)CharacterEnum.An].weaponId = characterInitialize.anInitializeWeapon.id;
                _characterSave.charcterData[(int)CharacterEnum.JinLay].weaponId = characterInitialize.jinInitializeWeapon.id;
                _characterSave.charcterData[(int)CharacterEnum.Bina].weaponId = characterInitialize.binaInitializeWeapon.id;

                for (int i = 0; i < 3; i++)
                {
                    _characterSave.charcterData[i].health = 100;
                    _characterSave.containWeaponId.Add(_characterSave.charcterData[i].weaponId);
                }

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
            string chipsetJson = JsonUtility.ToJson(_chipsetSave);
            string characterJson = JsonUtility.ToJson(_characterSave);
            string userJson = JsonUtility.ToJson(_userData);

            File.WriteAllText(_mapSavePath, mapJson);
            File.WriteAllText(_chipsetSavePath, chipsetJson);
            File.WriteAllText(_characterSavePath, characterJson);
            File.WriteAllText(_userDataSavePath, userJson);
        }

        public void AllPlayerDead()
        {
            File.Delete(_mapSavePath);
            File.Delete(_chipsetSavePath);
            File.Delete(_characterSavePath);
            File.Delete(_characterSavePath);
            SceneManager.LoadScene(SceneName.CafeScene);
        }

        public void AddChipset(ushort id)
        {
            _chipsetSave.containChipsets.Add(id);
            Save();
        }

        public void AddWeapon(int id)
        {
            _characterSave.containWeaponId.Add(id);
            Save();
        }

        public bool IsWeaponExist(int id)
        {
            return _characterSave.containWeaponId.Contains(id);
        }
    }
}
