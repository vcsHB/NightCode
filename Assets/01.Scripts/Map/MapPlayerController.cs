using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapPlayerController : MonoBehaviour
    {
        public Dictionary<CharacterEnum, Vector2Int> characterPositions;

        public void Init()
        {
            characterPositions = new Dictionary<CharacterEnum, Vector2Int>();
            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                characterPositions.Add(character, Vector2Int.zero);
            }
        }

        public void ProgressMap(CharacterEnum character, int height)
        {
            Vector2Int newPosition = characterPositions[character];
            newPosition.y = height;
            characterPositions[character] = newPosition;
        }
    }
}
