using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    //[Serializable]
    //public class InventorySave
    //{
    //    public ChipsetGroupSO chipsetGroup;
    //    public List<ushort> containChipset;
    //    public List<Chipset> containChipsetInstance;

    //    public List<ChipsetData>[] characterChipsetInventory;

    //    public List<ushort> GetCharacterChipsetIdList(CharacterEnum character)
    //    {
    //        List<ushort> chipsetIdList = new List<ushort>();
    //        characterChipsetInventory[(int)character].ForEach(chipsetData => chipsetIdList.Add(chipsetData.chipset.info.id));
    //        return chipsetIdList;
    //    }

    //    public List<ChipsetData> GetCharacterInventoryData(CharacterEnum character)
    //        => characterChipsetInventory[(int)character];

    //    public List<ChipsetSO> GetCharacterChipsetSOList(CharacterEnum character)
    //        => GetCharacterChipsetIdList(character).ConvertAll(chipsetId => chipsetGroup.GetChipset(chipsetId));

    //    public List<Chipset> GetCharacterChipsetIndex(CharacterEnum character)
    //        => characterChipsetInventory[(int)character].ConvertAll(data => data.chipset);

    //    public void AddChipset(CharacterEnum character, ChipsetData chipsetData)
    //    {
    //        characterChipsetInventory[(int)character].Add(chipsetData);
    //    }

    //    public InventorySave(ChipsetGroupSO chipsetGroup, List<ushort> containChipset, List<ChipsetData>[] characterChipsetIndex)
    //    {
    //        this.chipsetGroup = chipsetGroup;
    //        this.containChipset = containChipset;
    //        this.characterChipsetInventory = characterChipsetIndex;
    //        this.containChipsetInstance = new List<Chipset>();
    //    }
    //}

    [Serializable]
    public class ChipsetData
    {
        public int chipsetIndex;

        public Vector2Int center;
        public int rotate;

        public ChipsetData() { }

        public ChipsetData(int chipsetIndex, Vector2Int center, int rotate)
        {
            this.chipsetIndex = chipsetIndex;
            this.center = center;
            this.rotate = rotate;
        }
    }
}
