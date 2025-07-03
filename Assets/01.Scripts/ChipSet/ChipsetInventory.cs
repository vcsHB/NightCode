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
        [SerializeField] private Transform _chipsetParent;

        private InventoryData _inventoryView;
        private InventorySave _inventoryModel;

        private int _selectedChipsetIndex;
        private ChipsetInventorySlot _selectedSlot;

        private bool _canInsertChipset;
        private List<Vector2Int> _selections;
        private GridLayoutGroup _layoutGroup;


        #region Property

        public CharacterEnum Character => _inventoryView.character;
        public int SelectedChipsetIndex => _selectedChipsetIndex;
        public Chipset SelectedChipset => _inventoryModel.containChipsetInstance[_selectedChipsetIndex];
        public RectTransform RectTrm => transform as RectTransform;

        #endregion


        #region Initialize

        public void Initialize(CharacterEnum character, InventorySave chipsetData, List<Vector2Int> openedInventory)
        {
            _inventoryView = new InventoryData(character, _inventorySize, openedInventory, _chipsetParent as RectTransform);
            InitSlot(openedInventory);
            SetInventoryData(chipsetData, openedInventory);

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
                    ChipsetInventorySlot slotObj = Instantiate(_chipsetSlotPrefab, _chipsetSlotParent);
                    Vector2Int position = new Vector2Int(x, y);
                    slotObj.name = $"Slot_{x}_{y}";
                    _inventoryView.slots[x, y] = slotObj.GetComponent<ChipsetInventorySlot>();

                    _inventoryView.slots[x, y].SetPosition(position);
                    _inventoryView.slots[x, y].onPointerEnter += OnPointerEnterHandler;
                    _inventoryView.slots[x, y].onPointerExit += OnPointerExitHandler;
                }
            }

            UpdateOpenSlot();
        }

        private void UpdateOpenSlot()
        {
            for (int y = 0; y < _inventorySize.y; y++)
            {
                for (int x = 0; x < _inventorySize.x; x++)
                {
                    _inventoryView.slots[x, y].SetEnableSlot(_inventoryView.openSlot.Contains(new Vector2Int(x, y)));
                }
            }
        }

        public void SetInventoryData(InventorySave inventoryData, List<Vector2Int> openSlot)
        {
            _inventoryView.openSlot = openSlot;
            UpdateOpenSlot();

            List<int> chipsetIndexList = inventoryData.GetCharacterChipsetIndex(Character);

            for (int i = 0; i < chipsetIndexList.Count; i++)
            {
                int index = chipsetIndexList[i];

                Chipset chipset = inventoryData.containChipsetInstance[index];
                ChipsetData data = inventoryData.GetCharacterInventoryData(Character)[i];

                chipset.SetRotation(data.rotate);
                _inventoryView.assignedChipsets.Add(index, (data.center, data.rotate));
            }

            _selectedChipsetIndex = -1;
        }

        public void DisableInventory()
        {
            _inventoryModel.containChipsetInstance.ForEach(chipset =>
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

            _inventoryView.assignedChipsets.Keys.ToList().ForEach(chipsetIndex =>
            {
                Chipset chipset = _inventoryModel.containChipsetInstance[chipsetIndex];
                chipset.SetActive(true);

                Vector2Int center = _inventoryView.assignedChipsets[chipsetIndex].center;
                chipset.SetPosition(TransformSlotPositionToCanvasPosition(center));
            });
        }

        #endregion


        #region ChipsetEvents

        public void OnPointerDownChipset(int chipsetIndex)
        {
            _selectedChipsetIndex = chipsetIndex;

            if (chipsetIndex != -1 && _inventoryView.assignedChipsets.ContainsKey(chipsetIndex))
            {
                Chipset chipset = _inventoryModel.containChipsetInstance[chipsetIndex];
                chipset.info.chipsetSize.ForEach(offset =>
                {
                    Vector2Int position = offset + _inventoryView.assignedChipsets[chipsetIndex].center;
                    _inventoryView.chipsets[position.x, position.y] = null;
                });
                chipset.SetPrevPosition(_inventoryView.assignedChipsets[chipsetIndex].center, _inventoryView.assignedChipsets[chipsetIndex].rotate);
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

                _selections.ForEach(selected => _inventoryView.slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
                _selections.Clear();
            }

            _selectedChipsetIndex = -1;
        }

        #endregion

        #region SlotEvents

        private void OnPointerEnterHandler(Vector2Int position)
        {
            _selectedSlot = _inventoryView.slots[position.x, position.y];
            if (_selectedChipsetIndex == -1)
            {
                _selectedSlot.SetSelection(true);
            }
            else
            {
                _canInsertChipset = GetPositionSelections(position, out _selections);
                _selections.ForEach(selected => _inventoryView.slots[selected.x, selected.y].SetChipsetSlotState(true, _canInsertChipset));
            }
        }

        private void OnPointerExitHandler(Vector2Int position)
        {
            if (_selectedChipsetIndex == -1)
            {
                _inventoryView.slots[position.x, position.y].SetSelection(false);
            }
            else
            {
                _selections.ForEach(selected => _inventoryView.slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
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
            Chipset chipset = _inventoryModel.containChipsetInstance[chipsetIndex];
            _inventoryView.SetChipset(selectPosition, chipsetIndex, chipset);

            onInsertChipset?.Invoke();
            _selectedChipsetIndex = -1;

            Vector2Int center = selectPosition - chipset.GetSelectOffset();
            _inventoryModel.AddChipset(Character, new ChipsetData(chipsetIndex, center, chipset.Rotation));
        }

        public void RemoveChipset(int chipsetindex)
        {
            _inventoryView.RemoveChipset(chipsetindex);
        }

        #endregion

        #region Save&Load


        public List<ChipsetData> GetChipsetData()
        {
            List<ChipsetData> chipsetDatas = new List<ChipsetData>();

            _inventoryView.assignedChipsets.Keys.ToList().ForEach(index =>
            {
                chipsetDatas.Add(new ChipsetData(index, _inventoryView.assignedChipsets[index].center, _inventoryView.assignedChipsets[index].rotate));
            });

            return chipsetDatas;
        }

        #endregion

        #region PositionConvertRegion

        private Vector2 GetCenterPostion(Vector2Int selectedPosition)
        {
            Vector2Int center = selectedPosition - SelectedChipset.GetSelectOffset();
            Vector2 centerPosition = TransformSlotPositionToCanvasPosition(center);

            return centerPosition;
        }

        private Vector2 TransformSlotPositionToCanvasPosition(Vector2Int slotPosition)
        {
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint
                (Camera.main, _inventoryView.slots[slotPosition.x, slotPosition.y].RectTrm.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle((_chipsetParent as RectTransform), screenPosition, Camera.main, out Vector2 canvasPosition);

            return canvasPosition;
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

                    if (_inventoryView.chipsets[positions[i].x, positions[i].y] != null) isValid = false;
                    if (_inventoryView.slots[positions[i].x, positions[i].y].IsEnableSlot == false) isValid = false;
                }
            }

            return isValid;
        }
    }
}
