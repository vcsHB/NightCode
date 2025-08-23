using Chipset;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DataControl
{
    [Serializable]
    public class CharacterSave
    {
        public int credit;
        public List<int> containWeaponList;
        public List<CharacterData> characterData;

        public bool clearEnteredStage = false;
        public bool failEnteredStage = false;

        public CharacterSave()
        {
            credit = 0;
            containWeaponList = new List<int>();
            characterData = new List<CharacterData>();
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                characterData.Add(new CharacterData());
            }
        }

        public List<CharacterEnum> GetCurrentCharacter(Vector2Int currentPosition)
        {
            List<CharacterEnum> characterList = new List<CharacterEnum>();

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (characterData[(int)character].characterPosition == currentPosition)
                    characterList.Add(character);
            }

            return characterList;
        }
    }

    [Serializable]
    public class CharacterData
    {
        public bool isPlayerDead;

        public int equipWeaponId;
        public float playerHealth;
        public Vector2Int characterPosition;

        public CharacterData()
        {
            playerHealth = 100f;
            equipWeaponId = 0;
            isPlayerDead = false;
            characterPosition = Vector2Int.zero;
        }
    }
}
