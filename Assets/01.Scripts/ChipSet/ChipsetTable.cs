using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private void Update()
        {
            //For Debuging
            if (Keyboard.current.lKey.wasPressedThisFrame)
                Save();
        }

        public void Init()
        {
            _inventory = new Dictionary<CharacterEnum, ChipsetInventory>();
            var inventoryList = GetComponentsInChildren<ChipsetInventory>();

            for (int i = 0; i < inventoryList.Length; i++)
            {
                _inventory.Add((CharacterEnum)i, inventoryList[i]);
                inventoryList[i].Init();
            }
            Load();
            for (int i = 0; i < inventoryList.Length; i++)
            {
                inventoryList[i].Dispose();
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

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                ChipsetInventory inventory = _inventory[character];
                inventorySave.SetChipsets(character, inventory.GetInventoryData());
            }

            string json = JsonUtility.ToJson(inventorySave);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                Save();
                return;
            }

            string json = File.ReadAllText(_path);
            inventorySave = JsonUtility.FromJson<ChipsetInventorySave>(json);

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                ChipsetInventory inventory = _inventory[character];
                inventory.SetInventoryData(inventorySave.GetChipsets(character), inventorySave.openInventory);
            }
        }
    }

    [Serializable]
    public struct ChipsetInventorySave
    {
        public List<Vector2Int> openInventory;

        public List<ChipsetSave> anChipset;
        public List<ChipsetSave> jinLayChipset;
        public List<ChipsetSave> binaChipset;

        public List<ChipsetSave> GetChipsets(CharacterEnum character)
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
        public void SetChipsets(CharacterEnum character, List<ChipsetSave> inventoryData)
        {
            switch (character)
            {
                case CharacterEnum.An:
                    anChipset = inventoryData;
                    break;
                case CharacterEnum.JinLay:
                    jinLayChipset = inventoryData;
                    break;
                case CharacterEnum.Bina:
                    binaChipset = inventoryData;
                    break;
            }
        }
    }

    [Serializable]
    public struct ChipsetSave
    {
        public ushort chipsetId;
        public Vector2Int center;
        public int rotate;
    }
}
