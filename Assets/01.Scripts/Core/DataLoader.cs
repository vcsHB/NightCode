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

            Dictionary<string, Action<string>> loadActions = new()
            {
                { _mapSavePath,         json => _mapSave = JsonUtility.FromJson<MapSave>(json) ?? new MapSave() },
                { _chipsetSavePath,     json => _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(json) ?? new ChipsetInventorySave() },
                { _characterSavePath,   json => _characterSave = JsonUtility.FromJson<CharacterSave>(json) ?? new CharacterSave() },
                { _userDataSavePath,    json => _userData = JsonUtility.FromJson<UserData>(json) ?? new UserData() },
            };

            foreach (var entry in loadActions)
            {
                string path = entry.Key;

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "{}");
                }

                string json = File.ReadAllText(path);
                entry.Value(json);
            }
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
