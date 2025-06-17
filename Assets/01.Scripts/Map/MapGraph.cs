using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGraph
    {
        public Dictionary<CharacterEnum, Vector2Int> characterOriginPosition
            = new Dictionary<CharacterEnum, Vector2Int>();
        public Dictionary<CharacterEnum, Vector2Int> characterCurrentPosition
            = new Dictionary<CharacterEnum, Vector2Int>();
        private List<MapNode>[] _nodeMap;

        public void Init(List<MapNode>[] nodes)
        {
            _nodeMap = nodes;
        }

        public void MoveCharacter(CharacterEnum character, Vector2Int position)
        {
            characterCurrentPosition[character] = position;
        }

        #region Getter

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
        public MapNode GetNode(Vector2Int position)
            => _nodeMap[position.x][position.y];

        public bool IsCharacterExsists(CharacterEnum character)
            => characterCurrentPosition.ContainsKey(character);

        #endregion
    }

    [Serializable]
    public struct HeightInfo
    {
        public List<float> positions;
    }
}