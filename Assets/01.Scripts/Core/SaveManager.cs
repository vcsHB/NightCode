using Office.CharacterSkillTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;

namespace StatSystem
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        private StatSave _statSave = new();
        private string _path => Path.Combine(Application.dataPath, "Save/TechTree.json");


        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        private void Load()
        {
            string json;
            if (File.Exists(_path) == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    TechTreeSave treeSave = new TechTreeSave();
                    treeSave.characterType = (CharacterEnum)i;
                    treeSave.openListGUID = new List<string>();

                    _statSave.treeSave.Add(treeSave);
                }

                json = JsonUtility.ToJson(_statSave);
                File.WriteAllText(_path, json);

                return;
            }

            json = File.ReadAllText(_path);
            _statSave = JsonUtility.FromJson<StatSave>(json);
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_statSave);
            File.WriteAllText(_path, json);
        }


        public void AddSaveStat(CharacterEnum characterType, NodeSO nodeToSave)
        {
            if (_statSave.treeSave[(int)characterType].openListGUID.Contains(nodeToSave.guid)) return;
            _statSave.treeSave[(int)characterType].openListGUID.Add(nodeToSave.guid);

            Save();
        }

        public TechTreeSave GetStatValue(CharacterEnum characterType)
        {
            for (int i = 0; i < _statSave.treeSave.Count; i++)
            {
                if (_statSave.treeSave[i].characterType == characterType)
                {
                    return _statSave.treeSave[i];
                }
            }

            return null;
        }
    }
}
