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

        private MapSave _mapSave;
        private ChipsetInventorySave _chipsetSave;

        protected override void Awake()
        {
             base.Awake();
            Load();

            GetCharacters().ForEach(character => Debug.Log(character));
        }

        public MapNodeSO GetCurrentMap()
        {
            if (_mapSave == null) Load();
            return mapGraph.GetNodeSO(_mapSave.enterStageId);
        }

        public List<CharacterEnum> GetCharacters()
        {
            List<CharacterEnum> characters = new List<CharacterEnum>();

            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if(_mapSave.characterPositions[(int)character] == _mapSave.enterStagePosition)
                {
                    characters.Add(character);
                }
            }
            return characters;
        }

        public List<ChipsetSO> GetChipset(CharacterEnum character)
        {
            if(_chipsetSave == null) Load();
            return _chipsetSave.GetChipsets(character).
                ConvertAll(save => chipsetGroup.GetChipset(save.chipsetId));
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

            _mapSave = JsonUtility.FromJson<MapSave>(mapJson);
            _chipsetSave = JsonUtility.FromJson<ChipsetInventorySave>(chipsetJson);
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_mapSave);
            File.WriteAllText(_mapSavePath, json);
        }
    }
}
