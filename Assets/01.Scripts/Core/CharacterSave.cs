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
        public List<Vector2Int> openInventory;
        public List<int> containWeaponList;
        public List<ushort> containChipsetList;
        public List<CharacterData> characterData;

        public bool clearEnteredStage = false;
        public bool failEnteredStage = false;

        public CharacterSave()
        {
            credit = 0;
            openInventory = new List<Vector2Int>();
            containWeaponList = new List<int>();
            containChipsetList = new List<ushort>();
            characterData = new List<CharacterData>();
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                characterData.Add(new CharacterData());
            }
        }

        public List<ChipsetData> GetCharacterChipset(CharacterEnum character)
            => characterData[(int)character].chipsetInventoryData;

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

        public float playerHealth;
        public Vector2Int characterPosition;

        public int equipWeaponId;
        public List<ChipsetData> chipsetInventoryData;

        public CharacterData()
        {
            equipWeaponId = 0;
            isPlayerDead = false;
            chipsetInventoryData = new List<ChipsetData>();
            characterPosition = Vector2Int.zero;
        }
    }
}
