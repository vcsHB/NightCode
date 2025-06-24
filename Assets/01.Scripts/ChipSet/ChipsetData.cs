using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    [Serializable]
    public class ChipsetData
    {
        public ChipsetGroupSO chipsetGroup;
        public List<ushort> containChipset;
        public List<Chipset> containChipsetInstance;

        public List<CharacterChipsetData>[] characterChipsetInventory;

        public List<ushort> GetCharacterChipsetIdList(CharacterEnum character)
        {
            List<ushort> chipsetIdList = new List<ushort>();
            characterChipsetInventory[(int)character].ForEach(chipsetIndex =>
            {
                chipsetIdList.Add(containChipset[chipsetIndex.chipsetIndex]);
            });
            return chipsetIdList;
        }

        public List<CharacterChipsetData> GetCharacterInventoryData(CharacterEnum character)
            => characterChipsetInventory[(int)character];

        public List<ChipsetSO> GetCharacterChipsetSOList(CharacterEnum character)
            => GetCharacterChipsetIdList(character).ConvertAll(chipsetId => chipsetGroup.GetChipset(chipsetId));

        public List<int> GetCharacterChipsetIndex(CharacterEnum character)
            => characterChipsetInventory[(int)character].ConvertAll(data => data.chipsetIndex);

        public ChipsetData(ChipsetGroupSO chipsetGroup, List<ushort> containChipset, List<CharacterChipsetData>[] characterChipsetIndex)
        {
            this.chipsetGroup = chipsetGroup;
            this.containChipset = containChipset;
            this.characterChipsetInventory = characterChipsetIndex;
            this.containChipsetInstance = new List<Chipset>();
        }
    }

    [Serializable]
    public class CharacterChipsetData
    {
        public int chipsetIndex;

        public Vector2Int center;
        public int rotate;

        public CharacterChipsetData() {  }

        public CharacterChipsetData(int chipsetIndex, Vector2Int center, int rotate)
        {
            this.chipsetIndex = chipsetIndex;
            this.center = center;
            this.rotate = rotate;
        }
    }
}
