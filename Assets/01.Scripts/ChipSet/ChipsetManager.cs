using Core.DataControl;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Chipset
{
    public class ChipsetManager : MonoSingleton<ChipsetManager>
    {
        [SerializeField] private CharacterDataInitializeSO _initializeSO; 
        [SerializeField] private ChipsetGroupSO _chipsetGroupSO;
        [SerializeField] private ChipsetTable _chipetTable;
        [SerializeField] private ChipsetContainer _chipsetContainer;

        private ChipsetSave _save;
        private JsonLoadHelper<ChipsetSave> _chipsetLoadHelper;
        private readonly string _chipsetSavePath = Path.Combine(Application.dataPath, "Save/Chipset.json");

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            _chipsetLoadHelper = new JsonLoadHelper<ChipsetSave>(_chipsetSavePath);
            Load();
        }

        public Chipset GetChipset(int chipsetIndex)
            => _chipsetContainer.GetChipset(chipsetIndex);

        #region Save&Load

        public void Save()
        {
            _save = new ChipsetSave();
            _save.openInventory = _chipetTable.GetOpenedInventorySlots();
            _save.containChipset = _chipsetContainer.GetContainChipsetIdList();

            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                InventorySave inventorySave = new InventorySave();
                inventorySave.chipsetList = _chipetTable.GetInventory(character).GetChipsetData();
                _save.inventorySave.Add(inventorySave);
            }

            _chipsetLoadHelper.Save(_save);
        }

        public void Load()
        {
            _save = _chipsetLoadHelper.Load();
            if (_save.containChipset.Count == 0) InitializeChipsetData();

            _chipsetContainer.Initialize(_chipsetGroupSO, _save.containChipset, _save.inventorySave);
            _chipetTable.Initialize(_save.openInventory, _save.inventorySave);
            _chipetTable.onSelectInventory += _chipsetContainer.SetInventory;
        }

        private void InitializeChipsetData()
        {
            _save.containChipset = _initializeSO.containChipsets.ConvertAll(chipset => chipset.id);
            _save.openInventory = _initializeSO.openInventory;

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                InventorySave inventorySave = new InventorySave();
                inventorySave.chipsetList = new List<ChipsetData>();
                _save.inventorySave.Add(inventorySave);
            }
        }

        #endregion
    }


    [Serializable]
    public class ChipsetSave
    {
        public List<Vector2Int> openInventory = new();
        public List<ushort> containChipset = new();
        public List<InventorySave> inventorySave = new();

    }

    [Serializable]
    public class InventorySave
    {
        public List<ChipsetData> chipsetList;
    }
}
