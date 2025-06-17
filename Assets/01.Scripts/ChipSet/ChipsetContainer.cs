using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ChipsetGruopSO chipsetGroup;
        [Space]
        public List<int> usedGlobalChipsetIndex;
        public List<ChipsetSO> containChipset;
        public List<Chipset> chipsetInstanceContainer;
        public ChipsetInventory inventory;

        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        [SerializeField] private CanvasGroup _dragPanel;

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

            for (int i = 0; i < chipsetInstanceContainer.Count; i++)
            {
                Chipset chipset = chipsetInstanceContainer[i];
                inventory.AssignChipsetToInventory(chipset);
            }

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
            if(inventory.SelectedChipsetIndex != -1) _dragPanel.alpha = 1;
            inventory.onReturnChipset += SetAssignChipset;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _dragPanel.alpha = 0;
            inventory.onReturnChipset -= SetAssignChipset;
        }

        private void SetAssignChipset()
        {
            if (inventory.SelectedChipsetIndex == -1) return;

            Chipset chipset = ChipsetManager.Instance.GetChipset(inventory.SelectedChipsetIndex);
            if (chipset.IsForcePointerDown) return;
            chipset.SetActive(false);

            if (usedGlobalChipsetIndex.Contains(inventory.SelectedChipsetIndex))
                usedGlobalChipsetIndex.Remove(inventory.SelectedChipsetIndex);

            ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
            chipsetInfo.Init(inventory, chipset);
            chipsetInfo.onInsertChipset += OnInsertChipset;
            _chipsetInfos.Add(chipset, chipsetInfo);

            inventory.RemoveChipset(inventory.SelectedChipsetIndex);
            inventory.OnPointerDownChipset(-1);
            _dragPanel.alpha = 0;
        }
    }

}
