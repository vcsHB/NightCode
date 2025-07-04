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
        private List<Vector2Int> _selections;
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
                    _slot[x, y].onPointerEnter += OnPointerEnterHandler;
                    _slot[x, y].onPointerExit += OnPointerExitHandler;
                }
            }

            for(int i = 0; i < opened.Count; i++)
                _inventoryInfo.ActiveInventorySlot(opened[i]);
        }

        public void SetInventoryData(List<ChipsetData> inventoryData)
        {
            for (int i = 0; i < inventoryData.Count; i++)
            {
                ChipsetData data = inventoryData[i];
                Chipset chipset = _containChipset[data.chipsetIndex];

                Vector2 localPosition = InventoryPositionConverter.GetALocalPositionAtBPosition(_slot[data.center.x, data.center.y].RectTrm, _chipsetParent);
                _assignedChipsets.Add(data.chipsetIndex, (data.center, data.rotate));
                _inventoryInfo.TrySetChipset(chipset, data.center);
            }

            _selectedChipsetIndex = -1;
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

        public void EnableInventory()
        {
            gameObject.SetActive(true);
            StartCoroutine(DelaySetChipsetPosition());
        }

        private IEnumerator DelaySetChipsetPosition()
        {
            yield return null;

            _assignedChipsets.Keys.ToList().ForEach(chipsetIndex =>
            {
                Chipset chipset = _containChipset[chipsetIndex];
                chipset.SetActive(true);

                Vector2Int center = _assignedChipsets[chipsetIndex].center;
                chipset.SetPosition(TransformSlotPositionToCanvasPosition(center));
            });
        }

        #endregion


        #region ChipsetEvents

        private void HandleActiveSlot(Vector2Int position)
            => _slot[position.x, position.y].SetEnableSlot(true);

        public void OnPointerDownChipset(int chipsetIndex)
        {
            _selectedChipsetIndex = chipsetIndex;

            if (chipsetIndex != -1 && _assignedChipsets.ContainsKey(chipsetIndex))
            {
                Chipset chipset = _containChipset[chipsetIndex];
                chipset.info.chipsetSize.ForEach(offset =>
                {
                    Vector2Int position = offset + _assignedChipsets[chipsetIndex].center;
                    _inventoryInfo.RemoveChipset(position);
                });
                chipset.SetPrevPosition(_assignedChipsets[chipsetIndex].center, _assignedChipsets[chipsetIndex].rotate);
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
                if (_canInsertChipset)
                {
                    InsertChipset(_selectedSlot.SlotPosition, _selectedChipsetIndex);
                }
                else
                {
                    ReturnChipsetToPrevPosition();
                }

                //_selections.ForEach(selected => _inventoryInfo.slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
                _selections.Clear();
            }

            _selectedChipsetIndex = -1;
        }

        #endregion

        #region SlotEvents

        private void OnPointerEnterHandler(Vector2Int position)
        {
            //_selectedSlot = _inventoryInfo.slots[position.x, position.y];
            if (_selectedChipsetIndex == -1)
            {
                _selectedSlot.SetSelection(true);
            }
            else
            {
                _canInsertChipset = GetPositionSelections(position, out _selections);
                //_selections.ForEach(selected => _inventoryInfo.slots[selected.x, selected.y].SetChipsetSlotState(true, _canInsertChipset));
            }
        }

        private void OnPointerExitHandler(Vector2Int position)
        {
            if (_selectedChipsetIndex == -1)
            {
               // _inventoryInfo.slots[position.x, position.y].SetSelection(false);
            }
            else
            {
                //_selections.ForEach(selected => _inventoryInfo.slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
                _selections.Clear();
            }
            _selectedSlot = null;
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
            //Chipset chipset = _inventorySave.containChipsetInstance[chipsetIndex];
            //_inventoryInfo.SetChipset(selectPosition, chipsetIndex, chipset);

            onInsertChipset?.Invoke();
            _selectedChipsetIndex = -1;

            //Vector2Int center = selectPosition - chipset.GetSelectOffset();
           // _inventorySave.AddChipset(Character, new ChipsetData(chipsetIndex, center, chipset.Rotation));
        }

        public void RemoveChipset(int chipsetindex)
        {
            //_inventoryInfo.RemoveChipset(chipsetindex);
        }

        #endregion

        #region Save&Load

        public List<ChipsetData> GetChipsetData()
        {
            List<ChipsetData> chipsetDatas = new List<ChipsetData>();

           // _inventoryInfo.assignedChipsets.Keys.ToList().ForEach(index =>
            //{
            //    chipsetDatas.Add(new ChipsetData(index, _inventoryInfo.assignedChipsets[index].center, _inventoryInfo.assignedChipsets[index].rotate));
            //});

            return chipsetDatas;
        }

        #endregion

        #region PositionConvertRegion

        private Vector2 TransformSlotPositionToCanvasPosition(Vector2Int slotPosition)
        {
            //Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint
            //(Camera.main, _inventoryInfo.slots[slotPosition.x, slotPosition.y].RectTrm.position);

            //RectTransformUtility.ScreenPointToLocalPointInRectangle((_chipsetParent as RectTransform), screenPosition, Camera.main, out Vector2 canvasPosition);

            //return canvasPosition;
            return Vector2.zero;
        }

        #endregion

        private bool GetPositionSelections(Vector2Int originPosition, out List<Vector2Int> positions)
        {
            positions = new List<Vector2Int>();

            if (_selectedChipsetIndex == -1)
            {
                positions.Add(originPosition);
                return true;
            }

            Vector2Int offset = Vector2Int.zero;
            bool isValid = true;
            positions = SelectedChipset.GetOffsets();

            //인벤토리 바깥으로 튀어나갔는지 확인 & 인벤토리 안으로 위치 조정
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] += originPosition;

                if (isValid == false) continue;

                if (positions[i].x < 0)
                {
                    if (offset.x > 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.x = Mathf.Min(offset.x, positions[i].x);
                }
                if (positions[i].y < 0)
                {
                    if (offset.y > 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.y = Mathf.Min(offset.y, positions[i].y);
                }
                if (positions[i].x >= _inventorySize.x)
                {
                    if (offset.x < 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.x = Mathf.Max(offset.x, positions[i].x - (_inventorySize.x - 1));
                }
                if (positions[i].y >= _inventorySize.y)
                {
                    if (offset.y < 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.y = Mathf.Max(offset.y, positions[i].y - (_inventorySize.y - 1));
                }
            }

            if (isValid)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i] -= offset;

                    //if (_inventoryInfo.chipsets[positions[i].x, positions[i].y] != null) isValid = false;
                    //if (_inventoryInfo.slots[positions[i].x, positions[i].y].IsEnableSlot == false) isValid = false;
                }
            }

            return isValid;
        }
    }
}
