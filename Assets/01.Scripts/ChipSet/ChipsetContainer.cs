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
        public ChipsetGruopSO chipsetGroup;
        [Space]
        public List<ushort> usedGlobalChipsetId;
        public List<ChipsetSO> containChipset;
        public List<Chipset> chipsetInstanceContainer;
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
            chipsetInstanceContainer = new List<Chipset>();

            foreach (ChipsetSO chipsetSO in initializeChipset)
            {
                AddChipset(chipsetSO);
            }
        }

        public void OnChangeInventory(List<int> chipsetIndex)
        {
            foreach (var chipset in _chipsetInfos.Keys)
            {
                Destroy(_chipsetInfos[chipset].gameObject);
            }
            _chipsetInfos.Clear();

            for (int i = 0; i < chipsetInstanceContainer.Count; i++)
            {
                if (chipsetIndex.Contains(i))
                {
                    chipsetInstanceContainer[i].transform.localScale = Vector3.one;
                    continue;
                }

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(inventory, chipsetInstanceContainer[i]);
                chipsetInfo.onInsertChipset += OnInsertChipset;
                _chipsetInfos.Add(chipsetInstanceContainer[i], chipsetInfo);
            }
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            this.inventory = inventory;
            _chipsetInfos.Keys.ToList().ForEach(chipset =>
            {
                inventory.AssignChipsetToInventory(chipset);
                _chipsetInfos[chipset].Init(inventory, chipset);
            });

            OnChangeInventory(inventory.GetInventoryData().ConvertAll(save => save.chipsetindex));
        }

        public void AddChipset(ChipsetSO chipsetSO)
        {
            Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
            chipset.SetIndex(containChipset.Count);

            containChipset.Add(chipsetSO);
            chipsetInstanceContainer.Add(chipset);
            if (inventory != null) inventory.AssignChipsetToInventory(chipset);

            ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
            chipsetInfo.Init(inventory, chipset);
            chipsetInfo.onInsertChipset += OnInsertChipset;
            _chipsetInfos.Add(chipset, chipsetInfo);
        }

        private void OnInsertChipset(Chipset chipset)
        {
            if (chipset.info.isGlobalChipset)
                containChipset.Remove(chipset.info);

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
            if (inventory.SelectedChipsetIndex == -1) return;

            Chipset chipset = ChipsetManager.Instance.GetChipset(inventory.SelectedChipsetIndex);
            if (chipset.IsForcePointerDown) return;

            if (chipset.info.isGlobalChipset == false)
                containChipset.Remove(chipset.info);

            AddChipset(chipset.info);
            inventory.RemoveChipset(inventory.SelectedChipsetIndex);
            inventory.OnPointerDownChipset(-1);
        }
    }

}
