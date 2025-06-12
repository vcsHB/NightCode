using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public List<ChipsetSO> containChipset;
        public ChipsetInventory inventory;

        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;

        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;

        public void Init(List<ChipsetSO> initializeChipset)
        {
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();
            containChipset = new List<ChipsetSO>();

            foreach(ChipsetSO chipsetSO in initializeChipset)
            {
                AddChipset(chipsetSO);
            }
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            this.inventory = inventory;
            _chipsetInfos.Keys.ToList().ForEach(chipset =>
            {
                inventory.AddAssignedChipset(chipset);
                _chipsetInfos[chipset].Init(inventory, chipset);
            });
        }

        public void AddChipset(ChipsetSO chipsetSO)
        {
            containChipset.Add(chipsetSO);
            Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
            if (inventory != null) inventory.AddAssignedChipset(chipset);

            ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
            chipsetInfo.Init(inventory, chipset);
            chipsetInfo.onInsertChipset += OnInsertChipset;
            _chipsetInfos.Add(chipset, chipsetInfo);
        }

        private void OnInsertChipset(Chipset chipset)
        {
            _chipsetInfos[chipset].onInsertChipset -= OnInsertChipset;
            _chipsetInfos.Remove(chipset);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inventory.onReturnChipset += SetAssignChipset;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventory.onReturnChipset -= SetAssignChipset;
        }

        private void SetAssignChipset()
        {
            if (inventory.SelectedChipset == null || inventory.SelectedChipset.IsForcePointerDown) return;
            
            AddChipset(inventory.SelectedChipset.info);
            inventory.RemoveChipset(inventory.SelectedChipset, true);
            inventory.OnPointerDownChipset(null);
        }
    }

}
