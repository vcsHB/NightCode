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
        private TechTreeSave _treeSave = new();
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
                    _treeSave = new TechTreeSave();
                    _treeSave.openListGUID = new List<string>();
                }

                json = JsonUtility.ToJson(_treeSave);
                File.WriteAllText(_path, json);

                return;
            }

            json = File.ReadAllText(_path);
            _treeSave = JsonUtility.FromJson<TechTreeSave>(json);
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_treeSave);
            File.WriteAllText(_path, json);
        }


        public void AddSaveStat(CharacterEnum characterType, NodeSO nodeToSave)
        {
            if (_treeSave.openListGUID.Contains(nodeToSave.guid)) return;
            _treeSave.openListGUID.Add(nodeToSave.guid);

            Save();
        }

        public TechTreeSave GetStatValue()
        {
            return _treeSave;
        }
    }
}
