using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent<ChipsetSO> SetExplain;
        public UnityEvent UnSetExplain;

        [Space]
        public List<int> usedGlobalChipsetIndex;
        public ChipsetInventory currentInventory;
        public List<Chipset> container;

        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        [SerializeField] private CanvasGroup _dragPanel;

        private List<ushort> _containChipset;
        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;

        public void Initialize(ChipsetGroupSO chipsetGroupSO, List<ushort> containChipset)
        {
            _containChipset = containChipset;
            container = new List<Chipset>();
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();

            for (int i = 0; i < containChipset.Count; i++)
            {
                ChipsetSO chipsetSO = chipsetGroupSO.GetChipset(containChipset[i]);

                Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
                chipset.SetIndex(i);
                chipset.onSetExplain += HandleSetExplain;
                chipset.onUnSetExplain += HandleUnSetExplain;
                container.Add(chipset);

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(chipset);
                chipsetInfo.onSelectInventory += HandleSelectInventory;
                chipsetInfo.onUnSelectInventory += HandleUnSelectInventory;
                chipsetInfo.onReturnChipset += ReturnChipset;
                chipsetInfo.onInsertChipset += OnInsertChipset;
                chipsetInfo.onSetExplain += HandleSetExplain;
                chipsetInfo.onRemoveExplain += HandleUnSetExplain;
                _chipsetInfos.Add(chipset, chipsetInfo);
            }
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            this.currentInventory = inventory;

            for (int i = 0; i < container.Count; i++)
                inventory.AssignChipsetToInventory(container[i]);

            foreach (var chipset in _chipsetInfos.Keys)
            {
                _chipsetInfos[chipset].SetActive(true);
                _chipsetInfos[chipset].Init(chipset);
            }

            List<ChipsetData> chipsetData = inventory.GetChipsetData();
            for (int i = 0; i < chipsetData.Count; i++)
                _chipsetInfos[container[chipsetData[i].chipsetIndex]].SetActive(false);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentInventory.SelectedChipsetIndex != -1) _dragPanel.alpha = 1;
            currentInventory.onReturnChipset += SetChipsetToContainer;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _dragPanel.alpha = 0;
            currentInventory.onReturnChipset -= SetChipsetToContainer;
        }

        private void SetChipsetToContainer()
        {
            if (currentInventory.SelectedChipsetIndex == -1) return;

            Chipset chipset = container[currentInventory.SelectedChipsetIndex];
            if (chipset.IsForceMouseDown) return;
            chipset.SetActive(false);

            if (usedGlobalChipsetIndex.Contains(chipset.Index))
                usedGlobalChipsetIndex.Remove(chipset.Index);

            _chipsetInfos[chipset].SetActive(true);
            currentInventory.OnPointerDownChipset(-1);
            _dragPanel.alpha = 0;
        }

        #region EventHandler

        private void HandleSelectInventory(ChipsetInfo info)
        {
            info.AddAction(currentInventory);
        }

        private void HandleUnSelectInventory(ChipsetInfo info)
        {
            info.RemoveAction(currentInventory);
        }

        private void HandleSetExplain(ChipsetSO chipset)
        {
            SetExplain?.Invoke(chipset);
        }

        private void HandleUnSetExplain()
        {
            UnSetExplain?.Invoke();
        }

        private void OnInsertChipset(Chipset chipset)
        {
            if (chipset.info.isGlobalChipset)
                usedGlobalChipsetIndex.Add(chipset.Index);

            _chipsetInfos[chipset].SetActive(false);
        }

        private void ReturnChipset(Chipset chipset)
        {
            chipset.SetActive(false);

            if (usedGlobalChipsetIndex.Contains(chipset.Index))
                usedGlobalChipsetIndex.Remove(chipset.Index);

            _chipsetInfos[chipset].SetActive(true);
            currentInventory.SelectChipset(-1);
            _dragPanel.alpha = 0;
        }

        #endregion
    }
}
