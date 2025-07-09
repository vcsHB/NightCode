using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInventory : MonoBehaviour
    {
        public event Action onInsertChipset;
        public event Action onReturnChipset;

        [SerializeField] private Vector2Int _inventorySize;
        [SerializeField] private ChipsetInventorySlot _chipsetSlotPrefab;
        [SerializeField] private Transform _chipsetSlotParent;
        [SerializeField] private RectTransform _chipsetParent;

        private ChipsetInventorySlot[,] _slot;
        private Dictionary<int, (Vector2Int center, int rotate)> _assignedChipsets;
        private List<Chipset> _containChipset;

        private ChipsetInventoryInfo _inventoryInfo;

        private int _selectedChipsetIndex;
        private ChipsetInventorySlot _selectedSlot;

        private bool _canInsertChipset;
        private GridLayoutGroup _layoutGroup;


        #region Property

        public CharacterEnum Character => _inventoryInfo.Character;
        public int SelectedChipsetIndex => _selectedChipsetIndex;
        public Chipset SelectedChipset => _containChipset[_selectedChipsetIndex];
        public RectTransform RectTrm => transform as RectTransform;

        #endregion


        #region Initialize

        public void Initialize(CharacterEnum character, List<ChipsetData> chipsetData, List<Chipset> containChipset, List<Vector2Int> openedInventory)
        {
            _containChipset = containChipset;
            _slot = new ChipsetInventorySlot[_inventorySize.x, _inventorySize.y];
            _assignedChipsets = new Dictionary<int, (Vector2Int center, int rotate)>();
            _inventoryInfo = new ChipsetInventoryInfo(character, _inventorySize);
            _inventoryInfo.onActiveSlot += HandleActiveSlot;

            InitSlot(openedInventory);
            SetInventoryData(chipsetData);

            DisableInventory();
        }

        private void InitSlot(List<Vector2Int> opened)
        {
            _layoutGroup = _chipsetSlotParent.GetComponent<GridLayoutGroup>();

            RectTrm.sizeDelta = new Vector2(
                _inventorySize.x * _layoutGroup.cellSize.x
                + _layoutGroup.padding.left + _layoutGroup.padding.right,
                _inventorySize.y * _layoutGroup.cellSize.y
                + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            for (int y = 0; y < _inventorySize.y; y++)
            {
                for (int x = 0; x < _inventorySize.x; x++)
                {
                    ChipsetInventorySlot slot = Instantiate(_chipsetSlotPrefab, _chipsetSlotParent);
                    Vector2Int position = new Vector2Int(x, y);
                    slot.name = $"Slot_{x}_{y}";

                    _slot[x, y] = slot;
                    _slot[x, y].SetPosition(position);
                    _slot[x, y].onPointerEnter += OnPointerEnterToSlotHandler;
                    _slot[x, y].onPointerExit += OnPointerExitFromSlotHandler;
                }
            }

            for (int i = 0; i < opened.Count; i++)
                _inventoryInfo.ActiveInventorySlot(opened[i]);
        }

        public void SetInventoryData(List<ChipsetData> inventoryData)
        {
            for (int i = 0; i < inventoryData.Count; i++)
            {
                ChipsetData data = inventoryData[i];
                //Chipset chipset = _containChipset[data.chipsetIndex];

                //_inventoryInfo.TrySetChipset(chipset, data.center);
                InsertChipset(data.center, data.chipsetIndex);
            }

            _selectedChipsetIndex = -1;
        }

        public void EnableInventory()
        {
            gameObject.SetActive(true);
            StartCoroutine(DelaySetChipset());
        }

        public void DisableInventory()
        {
            _containChipset.ForEach(chipset =>
            {
                chipset.onSelectChipset -= OnPointerDownChipset;
                chipset.onPointerUpChipset -= OnPointerUpChipset;
                chipset.SetActive(false);
            });

            gameObject.SetActive(false);
        }

        private IEnumerator DelaySetChipset()
        {
            yield return null;

            _assignedChipsets.Keys.ToList().ForEach(chipsetIndex =>
            {
                Chipset chipset = _containChipset[chipsetIndex];
                chipset.SetActive(true);

                Vector2Int center = _assignedChipsets[chipsetIndex].center;
                chipset.SetPosition(InventoryPositionConverter.GetALocalPositionAtBPosition(_slot[center.x, center.y].RectTrm, _chipsetParent));
                _inventoryInfo.TrySetChipset(chipset, center);
            });
        }

        #endregion


        #region ChipsetEvents

        private void HandleActiveSlot(Vector2Int position)
            => _slot[position.x, position.y].SetEnableSlot(true);

        public void SelectChipset(int chipsetIndex)
        {
            //Debug.Log(chipsetIndex);
            _selectedChipsetIndex = chipsetIndex;
        }


        public void OnPointerDownChipset(int chipsetIndex)
        {
            SelectChipset(chipsetIndex);
            if (chipsetIndex == -1) return;

            if (_assignedChipsets.ContainsKey(chipsetIndex))
            {
                _containChipset[_selectedChipsetIndex].SetPrevPosition(_assignedChipsets[chipsetIndex].center, _assignedChipsets[chipsetIndex].rotate);
                RemoveChipset(chipsetIndex);
            }
        }

        public void OnPointerUpChipset()
        {
            if (_selectedSlot == null)
            {
                ReturnChipsetToPrevPosition();
            }
            else
            {
                SetChipsetSlotSelection(false);
                if (_canInsertChipset)
                {
                    InsertChipset(_selectedSlot.SlotPosition, _selectedChipsetIndex);
                }
                else
                {
                    ReturnChipsetToPrevPosition();
                }
            }

            SelectChipset(-1);
        }

        #endregion

        #region SlotEvents

        private void OnPointerEnterToSlotHandler(Vector2Int position)
        {
            _selectedSlot = _slot[position.x, position.y];
            if (_selectedChipsetIndex == -1)
            {
                _selectedSlot.SetSelection(true);
            }
            else
            {
                _canInsertChipset = _inventoryInfo.CanChipsetInsert(_containChipset[_selectedChipsetIndex], _selectedSlot.SlotPosition);
                SetChipsetSlotSelection(true);
                Debug.Log(_canInsertChipset);
            }
        }

        private void OnPointerExitFromSlotHandler(Vector2Int position)
        {
            if (_selectedChipsetIndex == -1)
            {
                _slot[position.x, position.y].SetSelection(false);
            }
            else
            {
                SetChipsetSlotSelection(false);
            }
            _selectedSlot = null;
        }

        private void SetChipsetSlotSelection(bool isSelected)
        {
            var offsets = InventoryPositionConverter.GetChipsetOffsets(_inventorySize, _selectedSlot.SlotPosition, _containChipset[_selectedChipsetIndex]);

            for (int i = 0; i < offsets.Count; i++)
                _slot[offsets[i].x, offsets[i].y].SetChipsetSlotState(isSelected, _canInsertChipset);
        }

        #endregion

        #region Inventory Functions

        public void AssignChipsetToInventory(Chipset chipset)
        {
            chipset.onSelectChipset -= OnPointerDownChipset;
            chipset.onSelectChipset += OnPointerDownChipset;
            chipset.onPointerUpChipset -= OnPointerUpChipset;
            chipset.onPointerUpChipset += OnPointerUpChipset;
        }

        private void ReturnChipsetToPrevPosition()
        {
            onReturnChipset?.Invoke();
            if (_selectedChipsetIndex == -1) return;

            InsertChipset(SelectedChipset.GetPrevPosition(), _selectedChipsetIndex);
        }

        public void InsertChipset(Vector2Int selectPosition, int chipsetIndex)
        {
            if (chipsetIndex == -1) return;
            Chipset chipset = _containChipset[chipsetIndex];
            _inventoryInfo.TrySetChipset(chipset, selectPosition);

            List<Vector2Int> positions = chipset.GetOffsets().ConvertAll(offset => selectPosition + offset);
            Vector2Int center = selectPosition - chipset.GetSelectOffset() - InventoryPositionConverter.GetChipsetOffset(_inventorySize, positions);
            chipset.SetPosition(InventoryPositionConverter.GetALocalPositionAtBPosition(_slot[center.x, center.y].RectTrm, _chipsetParent));

            onInsertChipset?.Invoke();
            _assignedChipsets.Add(chipsetIndex, (center, chipset.Rotation));
        }

        public void RemoveChipset(int chipsetIndex)
        {
            _inventoryInfo.RemoveChipset(_containChipset[chipsetIndex]);
            _assignedChipsets.Remove(chipsetIndex);
        }

        #endregion

        #region Save&Load

        public List<ChipsetData> GetChipsetData()
        {
            List<ChipsetData> chipsetDatas = new List<ChipsetData>();

            _assignedChipsets.Keys.ToList().ForEach(index =>
            {
                chipsetDatas.Add(new ChipsetData(index, _assignedChipsets[index].center, _assignedChipsets[index].rotate));
            });

            return chipsetDatas;
        }

        #endregion
    }
}
