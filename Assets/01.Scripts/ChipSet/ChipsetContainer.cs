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

        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        [SerializeField] private CanvasGroup _dragPanel;


        private InventorySave _chipsetData;
        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;

        public void Initialize(InventorySave chipsetData)
        {
            _chipsetData = chipsetData;
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();

            for (int i = 0; i < chipsetData.containChipset.Count; i++)
            {
                ChipsetSO chipsetSO = chipsetData.chipsetGroup.GetChipset(chipsetData.containChipset[i]);

                Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
                chipset.SetIndex(i);
                chipset.onSetExplain += HandleSetExplain;
                chipset.onUnSetExplain += HandleUnSetExplain;
                _chipsetData.containChipsetInstance.Add(chipset);

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(currentInventory, chipset);
                chipsetInfo.onReturnChipset += ReturnChipset;
                chipsetInfo.onInsertChipset += OnInsertChipset;
                chipsetInfo.onEnter += HandleSetExplain;
                chipsetInfo.onExit += HandleUnSetExplain;
                _chipsetInfos.Add(chipset, chipsetInfo);
            }
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            this.currentInventory = inventory;

            for (int i = 0; i < _chipsetData.containChipsetInstance.Count; i++)
            {
                Chipset chipset = _chipsetData.containChipsetInstance[i];
                inventory.AssignChipsetToInventory(chipset);
            }

            OnChangeInventory(inventory.Character);
        }

        private void OnChangeInventory(CharacterEnum character)
        {
            foreach (var chipset in _chipsetInfos.Keys)
            {
                _chipsetInfos[chipset].SetActive(true);
                _chipsetInfos[chipset].Init(currentInventory, chipset);
            }

            List<int> chipsetIndex = _chipsetData.GetCharacterChipsetIndex(character);
            for (int i = 0; i < chipsetIndex.Count; i++)
            {
                _chipsetInfos[_chipsetData.containChipsetInstance[chipsetIndex[i]]].SetActive(false);
            }
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
            {
                usedGlobalChipsetIndex.Add(chipset.Index);
            }

            _chipsetInfos[chipset].SetActive(false);
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

            Chipset chipset = _chipsetData.containChipsetInstance[currentInventory.SelectedChipsetIndex];
            if (chipset.IsForceMouseDown) return;
            chipset.SetActive(false);

            if (usedGlobalChipsetIndex.Contains(chipset.Index))
                usedGlobalChipsetIndex.Remove(chipset.Index);

            _chipsetInfos[chipset].SetActive(true);
            currentInventory.OnPointerDownChipset(-1);
            _dragPanel.alpha = 0;
        }

        private void ReturnChipset(Chipset chipset)
        {
            currentInventory.OnPointerDownChipset(-1);
            chipset.SetActive(false);

            if (usedGlobalChipsetIndex.Contains(chipset.Index))
                usedGlobalChipsetIndex.Remove(chipset.Index);

            _chipsetInfos[chipset].SetActive(true);
            currentInventory.OnPointerDownChipset(-1);
            _dragPanel.alpha = 0;
        }
    }

    public class ChipsetContainerData
    {
        public ChipsetGroupSO chipsetGroupSO;
        public Dictionary<Chipset, ChipsetInfo> chipsetInfos;
        public List<ushort> containChipset;
    }
}
