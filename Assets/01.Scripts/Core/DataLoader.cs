using Agents.Players.WeaponSystem;
using Chipset;
using Map;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.DataControl
{
    public class DataLoader : MonoSingleton<DataLoader>
    {
        public MapGraphSO mapGraph;
        public ChipsetGruopSO chipsetGroup;
        public PlayerWeaponListSO weaponList;
        private static string _mapSavePath = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private static string _chipsetSavePath = Path.Combine(Application.dataPath, "Save/Chipset.json");
        private static string _characterSavePath = Path.Combine(Application.dataPath, "Save/CharacterData.json");

        private MapSave _mapSave;
        private ChipsetInventorySave _chipsetSave;
        private CharacterSave _characterSave;

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
            string mapJson = File.ReadAllText(_mapSavePath);
            string chipsetJson = File.ReadAllText(_chipsetSavePath);
            string characterJson = File.ReadAllText(_characterSavePath);

            _mapSave = JsonUtility.FromJson<MapSave>(mapJson);
            _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(chipsetJson);
            _characterSave = JsonUtility.FromJson<CharacterSave>(characterJson);
        }

        public void Save()
        {
            string mapJson = JsonUtility.ToJson(_mapSave);
            string chipsetJson = JsonUtility.ToJson(_chipsetSave);
            string characterJson = JsonUtility.ToJson(_characterSave);

            File.WriteAllText(_mapSavePath, mapJson);
            File.WriteAllText(_chipsetSavePath, chipsetJson);
            File.WriteAllText(_characterSavePath, characterJson);
        }

        public void AllPlayerDead()
        {
            File.Delete(_mapSavePath);
            File.Delete(_chipsetSavePath);
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
