using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Space]
        public ChipsetInventory currentInventory;

        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        [SerializeField] private CanvasGroup _dragPanel;

        private List<Chipset> _container;
        private List<Chipset> _chipsetList;
        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;

        private ScrollRect _scrollRect;
        private float _infoWidth = 230f;
        private float _pedding = 20f;
        private float _spacing = 5f;

        public void Initialize(ChipsetGroupSO chipsetGroupSO, List<ushort> containChipset, List<InventorySave> inventoryInfo)
        {
            List<int> excludeIndex = new();

            for(int i =0; i< inventoryInfo.Count; i++)
            {
                for(int j = 0; j < inventoryInfo[i].chipsetList.Count; j++)
                {
                    excludeIndex.Add(inventoryInfo[i].chipsetList[j].chipsetIndex);
                }
            }

            _container = new List<Chipset>();
            _chipsetList = new List<Chipset>();
            _scrollRect = GetComponentInChildren<ScrollRect>();
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();

            for (int i = 0; i < containChipset.Count; i++)
            {
                ChipsetSO chipsetSO = chipsetGroupSO.GetChipset(containChipset[i]);

                Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
                chipset.SetIndex(i);
                _chipsetList.Add(chipset);

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(chipset);
                chipsetInfo.onSelectInventory += HandleSelectInventory;
                chipsetInfo.onUnSelectInventory += HandleUnSelectInventory;
                chipsetInfo.onReturnChipset += ReturnChipset;
                chipsetInfo.onInsertChipset += OnInsertChipset;
                _chipsetInfos.Add(chipset, chipsetInfo);

                if(excludeIndex.Contains(i))  chipsetInfo.SetActive(false);
                else _container.Add(chipset);
            }
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            // assign chipset at the inventory
            for (int i = 0; i < _chipsetList.Count; i++)
            {
                currentInventory?.UnAssignChipsetToInventory(_chipsetList[i]);
                inventory?.AssignChipsetToInventory(_chipsetList[i]);
            }

            currentInventory?.InventoryInfo.containChipsetIndex.ForEach(chipset => _chipsetList[chipset].SetActive(false));
            inventory.InventoryInfo.containChipsetIndex.ForEach(chipset => _chipsetList[chipset].SetActive(true));
            this.currentInventory = inventory;

            //int infoCount = 0;
            //foreach (var chipset in _chipsetInfos.Keys)
            //{
            //    _chipsetInfos[chipset].SetActive(true);
            //    _chipsetInfos[chipset].Init(chipset);
            //    ++infoCount;
            //}

            //List<ChipsetData> chipsetData = inventory.GetChipsetData();
            //for (int i = 0; i < chipsetData.Count; i++)
            //{
            //    _chipsetInfos[_container[chipsetData[i].chipsetIndex]].SetActive(false);
            //    --infoCount;
            //}

            //float width = _pedding + _infoWidth * infoCount + _spacing * (infoCount - 1);
            //_scrollRect.content.sizeDelta = new Vector2(width, _scrollRect.content.sizeDelta.y);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentInventory.SelectedChipset != null) _dragPanel.alpha = 1;
            currentInventory.onReturnChipset += SetChipsetToContainer;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _dragPanel.alpha = 0;
            currentInventory.onReturnChipset -= SetChipsetToContainer;
        }

        private void SetChipsetToContainer()
        {
            if (currentInventory.SelectedChipset == null) return;

            Chipset chipset = currentInventory.SelectedChipset;
            if (chipset.IsForceMouseDown) return;
            chipset.SetActive(false);
            _container.Add(chipset);

            _chipsetInfos[chipset].SetActive(true);
            currentInventory.OnPointerDownChipset(-1);
            _dragPanel.alpha = 0;
            _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x + _infoWidth + _spacing, _scrollRect.content.sizeDelta.y);
        }

        public List<ushort> GetContainChipsetIdList()
        {
            return _chipsetList.ConvertAll(chipset => chipset.info.id);
        }

        #region EventHandler

        private void HandleSelectInventory(ChipsetInfo info)
        {
            _chipsetInfos.Keys.ToList().ForEach(chipset =>
            {
                if (_chipsetInfos[chipset] == info)
                {
                    chipset.SetActive(true);
                }
            });

            _scrollRect.horizontal = false;
            info.AddAction(currentInventory);
        }

        private void HandleUnSelectInventory(ChipsetInfo info)
        {
            _scrollRect.horizontal = true;
            info.RemoveAction(currentInventory);
        }

        private void OnInsertChipset(Chipset chipset)
        {
            _container.Remove(chipset);
            _chipsetInfos[chipset].SetActive(false);
            _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x - _infoWidth - _spacing, _scrollRect.content.sizeDelta.y);
        }

        private void ReturnChipset(Chipset chipset)
        {
            chipset.SetActive(false);
            _chipsetInfos[chipset].SetActive(true);

            currentInventory.SelectChipset(-1);
            currentInventory.RemoveChipset(chipset.Index);
            _dragPanel.alpha = 0;
        }

        public Chipset GetChipset(int chipsetIndex)
        {
            if (chipsetIndex == -1) return null;
            return _chipsetList[chipsetIndex];
        }

        #endregion
    }
}
