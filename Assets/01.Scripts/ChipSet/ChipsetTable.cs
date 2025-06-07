using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Chipset
{
    public class ChipsetTable : MonoBehaviour
    {
        private ChipsetInventory _selectedInventory;
        public ChipsetContainer _container;
        public Dictionary<CharacterEnum, ChipsetInventory> _inventory;
        public List<Vector2Int> openInventory;

        private string _path = Path.Combine(Application.dataPath, "Save/Chipset.json");
        private ChipsetInventorySave inventorySave;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _inventory = new Dictionary<CharacterEnum, ChipsetInventory>();
            var inventoryList = GetComponentsInChildren<ChipsetInventory>();

            for (int i = 0; i < inventoryList.Length; i++)
            {
                _inventory.Add((CharacterEnum)i, inventoryList[i]);
                inventoryList[i].Init();
                inventoryList[i].gameObject.SetActive(false);
            }

            _container.Init();
            SelectInventory(CharacterEnum.An);
        }

        public void SelectInventory(CharacterEnum character)
        {
            if (_selectedInventory != null) _selectedInventory.Dispose();
            _inventory[character].gameObject.SetActive(true);
            _selectedInventory = _inventory[character];
            _selectedInventory.InitChipset();

            _container.SetInventory(_selectedInventory);
        }

        public void Save()
        {
            inventorySave = new ChipsetInventorySave();
            inventorySave.openInventory = openInventory;

            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                ChipsetInventory inventory = _inventory[character];
                List<ChipsetInfo> chipsetInfos = new List<ChipsetInfo>();
                
                inventory.
                
                chipsetInfos.Add();
            }
        }

        public void Load()
        {

        }
    }

    [Serializable]
    public struct ChipsetInventorySave
    {
        public List<Vector2Int> openInventory;

        public List<ChipsetInfo> anChipset;
        public List<ChipsetInfo> jinLayChipset;
        public List<ChipsetInfo> binaChipset;

        public List<ChipsetInfo> GetChipsets(CharacterEnum character)
        {
            switch (character)
            {
                case CharacterEnum.An:
                    return anChipset;
                case CharacterEnum.JinLay:
                    return jinLayChipset;
                case CharacterEnum.Bina:
                    return binaChipset;
            }
            return null;
        }
    }

    [Serializable]
    public struct ChipsetInfo
    {
        public int chipsetId;
        public Vector2Int center;
        public int rotate;
    }
}
