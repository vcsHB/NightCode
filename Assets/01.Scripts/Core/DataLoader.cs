using Agents.Players.WeaponSystem;
using Chipset;
using Map;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.DataControl
{
    public class DataLoader : MonoSingleton<DataLoader>
    {
        public MapGraphSO mapGraph;
        public ChipsetGruopSO chipsetGroup;
        private static string _mapSavePath = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private static string _chipsetSavePath = Path.Combine(Application.dataPath, "Save/Chipset.json");
        private static string _characterSavePath = Path.Combine(Application.dataPath, "Save/CharacterData.json");

        private MapSave _mapSave;
        private ChipsetInventorySave _chipsetSave;
        private CharacterSave _weaponData;

        protected override void Awake()
        {
            base.Awake();
            Load();
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
            Save();
        }

        public void Load()
        {
            string mapJson = File.ReadAllText(_mapSavePath);
            string chipsetJson = File.ReadAllText(_chipsetSavePath);
            string characterJson = File.ReadAllText(_characterSavePath);

            _mapSave = JsonUtility.FromJson<MapSave>(mapJson);
            _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(chipsetJson);
            _weaponData = JsonUtility.FromJson<CharacterSave>(characterJson);
        }

        public void Save()
        {
            string mapJson = JsonUtility.ToJson(_mapSave);
            string chipsetJson = JsonUtility.ToJson(_chipsetSave);
            string characterJson = JsonUtility.ToJson(_weaponData);

            File.WriteAllText(_mapSavePath, mapJson);
            File.WriteAllText(_chipsetSavePath, chipsetJson);
            File.WriteAllText(_characterSavePath, characterJson);
        }
    }
}
