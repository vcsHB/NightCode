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
        public ChipsetGruopSO chipsetGruopSO;
        public ChipsetContainer _container;
        public Dictionary<CharacterEnum, ChipsetInventory> _inventory;
        private ChipsetInventory _selectedInventory;
        private CharacterEnum _selectedCharacter;

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

            if (Keyboard.current.oKey.wasPressedThisFrame)
                Load();
        }

        public void Init()
        {
            _container.Init();
            _inventory = new Dictionary<CharacterEnum, ChipsetInventory>();
            var inventoryList = GetComponentsInChildren<ChipsetInventory>();

            for (int i = 0; i < inventoryList.Length; i++)
            {
                _inventory.Add((CharacterEnum)i, inventoryList[i]);
                inventoryList[i].Init();
                inventoryList[i].Dispose();
            }
            Load();
            SelectInventory(CharacterEnum.An);
        }

        public void SelectInventory(CharacterEnum character)
        {
            ChipsetInventory prevInventory = _selectedInventory;
            _selectedInventory = _inventory[character];
            _selectedInventory.gameObject.SetActive(true);
            StartCoroutine(DelayLoadData(character));
            //_selectedInventory.SetInventoryData(inventorySave.GetChipsets(character), inventorySave.openInventory);
            _container.SetInventory(_selectedInventory);

            Save(_selectedCharacter);
            _selectedCharacter = character;
            if (prevInventory != null && prevInventory != _selectedInventory) prevInventory.Dispose();
        }

        private IEnumerator DelayLoadData(CharacterEnum character)
        {
            yield return new WaitForSeconds(0.1f);
            _selectedInventory.SetInventoryData(inventorySave.GetChipsets(character), inventorySave.openInventory);
        }

        public void Save()
        {
            if (inventorySave == null) inventorySave = new ChipsetInventorySave();
            inventorySave.openInventory = openInventory;
            inventorySave.containChipsets = _container.containChipset.ConvertAll(chipset => chipset.id);

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                ChipsetInventory inventory = _inventory[character];
                inventorySave.SetChipsets(character, inventory.GetInventoryData());
            }

            string json = JsonUtility.ToJson(inventorySave);
            File.WriteAllText(_path, json);
        }

        public void Save(CharacterEnum character)
        {
            if (inventorySave == null) inventorySave = new ChipsetInventorySave();
            inventorySave.openInventory = openInventory;
            inventorySave.containChipsets = _container.containChipset.ConvertAll(chipset => chipset.id);

            ChipsetInventory inventory = _inventory[character];
            inventorySave.SetChipsets(character, inventory.GetInventoryData());

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
            inventorySave.containChipsets.ForEach(chipsetId => _container.AddChipset(chipsetGruopSO.GetChipset(chipsetId)));

            //foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            //{
            //    ChipsetInventory inventory = _inventory[character];
            //    inventory.SetInventoryData(inventorySave.GetChipsets(character), inventorySave.openInventory);
            //}
        }
    }

    [Serializable]
    public class ChipsetInventorySave
    {
        public List<ushort> containChipsets;
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
