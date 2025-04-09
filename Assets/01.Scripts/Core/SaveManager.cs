using Office.CharacterSkillTree;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StatSystem
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        private StatSave _statSave = new();
        private string _path => Path.Combine(Application.dataPath, "Save/TechTree.json");

        public void Save(CharacterEnum characterType, SkillTree tree)
        {
            _statSave.treeSave[(int)characterType] = tree.GetTreeSave();
            string json = JsonUtility.ToJson(_statSave);
            File.WriteAllText(_path, json);
        }

        public void AddSaveStat(CharacterEnum characterType, NodeSO nodeToSave)
        {
            Debug.Log(_statSave.treeSave.Count);
            if (_statSave.treeSave[(int)characterType].openListGUID.Contains(nodeToSave.guid)) return;
            _statSave.treeSave[(int)characterType].openListGUID.Add(nodeToSave.guid);

            string json = JsonUtility.ToJson(_statSave);
            File.WriteAllText(_path, json);
        }

        public TechTreeSave Load(CharacterEnum characterType)
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

                return _statSave.treeSave[(int)characterType];
            }

            json = File.ReadAllText(_path);
            _statSave = JsonUtility.FromJson<StatSave>(json);

            for (int i = 0; i < _statSave.treeSave.Count; i++)
            {
                Debug.Log(_statSave.treeSave[i].characterType);
                if (_statSave.treeSave[i].characterType == characterType)
                {
                    return _statSave.treeSave[i];
                }
            }

            return null;
        }
    }
}
