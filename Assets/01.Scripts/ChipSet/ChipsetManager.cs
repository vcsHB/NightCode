using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Chipset
{
    public class ChipsetManager : MonoSingleton<ChipsetManager>
    {
        public ChipsetGruopSO chipsetGroupSO;
        public ChipsetInitializeSO chipsetInitSO;

        public ChipsetContainer chipsetContainer;
        public ChipsetTable chipsetTable;

        private string _path = Path.Combine(Application.dataPath, "Save/Chipset.json");
        private ChipsetInventorySave inventorySave;

        protected override void Awake()
        {
            base.Awake();
            chipsetTable.onSelectInventory += SelectInventory;

            Load();
            chipsetContainer.Init(inventorySave.containChipsets.ConvertAll(chipsetId => chipsetGroupSO.GetChipset(chipsetId)));
            chipsetTable.Init(inventorySave);
        }

        private void SelectInventory(CharacterEnum character)
        {
            ChipsetInventory selectedInventory = chipsetTable.GetInventory(character);
            selectedInventory.EnableInventory();

            chipsetContainer.SetInventory(selectedInventory);
        }

        private void Init()
        {
            inventorySave = new ChipsetInventorySave();

            inventorySave.openInventory = chipsetInitSO.openInventory;
            inventorySave.containChipsets = chipsetInitSO.containChipsets.ConvertAll(Chipset => Chipset.id);

            inventorySave.anChipset = new List<ChipsetSave>();
            inventorySave.jinLayChipset = new List<ChipsetSave>();
            inventorySave.binaChipset = new List<ChipsetSave>();

            string json = JsonUtility.ToJson(inventorySave);
            File.WriteAllText(_path, json);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            if (inventorySave == null) inventorySave = new ChipsetInventorySave();

            inventorySave.openInventory = chipsetTable.GetOpenedInventorySlots();
            inventorySave.containChipsets = chipsetContainer.containChipset.ConvertAll(chipset => chipset.id);

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                ChipsetInventory inventory = chipsetTable.GetInventory(character);
                inventorySave.SetChipsets(character, inventory.GetInventoryData());
            }

            string json = JsonUtility.ToJson(inventorySave);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                Init();
                return;
            }

            string json = File.ReadAllText(_path);
            inventorySave = JsonUtility.FromJson<ChipsetInventorySave>(json);
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
