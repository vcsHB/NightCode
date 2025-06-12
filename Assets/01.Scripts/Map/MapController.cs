using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public Dictionary<CharacterEnum, Vector2Int> characterOriginPosition;
        public Dictionary<CharacterEnum, Vector2Int> characterCurrentPosition;
        private string _path = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private MapSave _save;

        public int CurrentChapter { get; private set; }
        public int CurrentDepth { get; private set; }

        public Vector2Int GetCharacterOriginPosition(CharacterEnum character)
            => characterOriginPosition[character];

        public Vector2Int GetCharcterCurrentPosition(CharacterEnum character)
            => characterCurrentPosition[character];

        public bool IsCharacterMoved(CharacterEnum character)
            => characterOriginPosition[character] != characterCurrentPosition[character];

        public bool IsCurrentPositionExsist(Vector2Int position) 
            => characterCurrentPosition.ContainsValue(position);

        public bool CheckConnectionExsist(Vector2Int from, Vector2Int to)
        {
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (characterOriginPosition[character] == from && 
                    characterCurrentPosition[character] == to)
                {
                    return true;
                }
            }
            return false;
        }

        public void Init()
        {
            CurrentDepth = 0;
            characterOriginPosition = new Dictionary<CharacterEnum, Vector2Int>();
            characterCurrentPosition = new Dictionary<CharacterEnum, Vector2Int>();
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                characterOriginPosition.Add(character, Vector2Int.zero);
                characterCurrentPosition.Add(character, Vector2Int.zero);
            }
            SaveMapSeed(UnityEngine.Random.Range(0, 1000000));
        }

        public void MoveCharacter(CharacterEnum character, Vector2Int position)
        {
            characterCurrentPosition[character] = position;
        }


        #region Save&Load

        public void SaveMapSeed(int seed)
        {
            if (_save == null) _save = new MapSave();
            _save.seed = seed;
            Save();
        }

        public void AddCompleteNode(Vector2Int nodePosition)
        {
            if (_save == null) _save = new MapSave();

            if (_save.completedNodes == null) _save.completedNodes = new List<Vector2Int>();
            _save.completedNodes.Add(nodePosition);
        }

        public int GetSeed()
        {
            if (_save == null) Load();
            return _save.seed;
        }

        public List<Vector2Int> GetCompleteNodes()
        {
            if (_save == null) Load();
            return _save.completedNodes ?? new List<Vector2Int>();
        }

        public void Save()
        {
            if (_save == null) _save = new MapSave();

            _save.characterPositions = new List<Vector2Int>();
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                _save.characterPositions.Add(characterCurrentPosition[character]);
            }

            string json = JsonUtility.ToJson(_save, true);
            File.WriteAllText(_path, json);
        }

        public bool Load()
        {
            if (File.Exists(_path) == false)
            {
                Init();
                Save();
                UnityEngine.Random.InitState(_save.seed);
                return false;
            }

            string json = File.ReadAllText(_path);
            _save = JsonUtility.FromJson<MapSave>(json);
            characterOriginPosition = new Dictionary<CharacterEnum, Vector2Int>();
            characterCurrentPosition = new Dictionary<CharacterEnum, Vector2Int>();

            CurrentDepth = int.MaxValue;
            for (int i = 0; i < _save.characterPositions.Count; i++)
            {
                characterOriginPosition.Add((CharacterEnum)i, _save.characterPositions[i]);
                characterCurrentPosition.Add((CharacterEnum)i, _save.characterPositions[i]);
                CurrentDepth = _save.characterPositions[i].x;
            }

            UnityEngine.Random.InitState(_save.seed);
            return true;
        }

        #endregion
    }

    [Serializable]
    public class MapSave
    {
        public int seed;
        public int _currentChapter;
        public List<Vector2Int> characterPositions;
        public List<Vector2Int> completedNodes;

        public MapSave()
        {
            seed = 0;
            _currentChapter = 0;
            characterPositions = new List<Vector2Int>();
            completedNodes = new List<Vector2Int>();
        }
    }

    [Serializable]
    public class LevelSave
    {
        public List<NodeSave> nodeSave;
    }

    [Serializable]
    public class NodeSave
    {
        public int nodeId;
        public List<Vector2Int> connections;
    }
}

