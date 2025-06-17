using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInventory : MonoBehaviour
    {
        public event Action onInsertChipset;
        public event Action onReturnChipset;

        public ChipsetGruopSO chipsetGroup;
        [SerializeField] private Vector2Int _inventorySize;
        [SerializeField] private ChipsetInventorySlot _slotPrefab;
        [SerializeField] private Transform _slotParent;
        [SerializeField] private Transform _chipsetParent;

        private List<Vector2Int> _openSlot;
        private GridLayoutGroup _layoutGroup;

        private List<Chipset> _exsistingChipset = new List<Chipset>();
        private Dictionary<int, (Vector2Int center, int rotate)> _assignedChipsets = new();
        private ChipsetInventorySlot[,] _slots;
        private Chipset[,] _chipsets;

        private int _selectedChipsetIndex;
        private ChipsetInventorySlot _selectedSlot;

        private bool _canInsertChipset;
        private List<Vector2Int> _selections;

        #region Property

        public int SelectedChipsetIndex => _selectedChipsetIndex;
        public Chipset SelectedChipset => ChipsetManager.Instance.GetChipset(_selectedChipsetIndex);
        private RectTransform RectTrm => transform as RectTransform;

        #endregion


        #region Initialize

        public void Init(List<Vector2Int> openedInventory)
        {
            _openSlot = openedInventory;
            _chipsets = new Chipset[_inventorySize.x, _inventorySize.y];
            InitSlot(_openSlot);
        }

        private void InitSlot(List<Vector2Int> opened)
        {
            _layoutGroup = _slotParent.GetComponent<GridLayoutGroup>();
            _slots = new ChipsetInventorySlot[_inventorySize.x, _inventorySize.y];

            RectTrm.sizeDelta = new Vector2(
                _inventorySize.x * _layoutGroup.cellSize.x
                + _layoutGroup.padding.left + _layoutGroup.padding.right,
                _inventorySize.y * _layoutGroup.cellSize.y
                + _layoutGroup.padding.top + _layoutGroup.padding.bottom);

            for (int y = 0; y < _inventorySize.y; y++)
            {
                for (int x = 0; x < _inventorySize.x; x++)
                {
                    ChipsetInventorySlot slotObj = Instantiate(_slotPrefab, _slotParent);
                    Vector2Int position = new Vector2Int(x, y);
                    slotObj.name = $"Slot_{x}_{y}";
                    _slots[x, y] = slotObj.GetComponent<ChipsetInventorySlot>();

                    _slots[x, y].SetPosition(position);
                    _slots[x, y].onPointerEnter += OnPointerEnterHandler;
                    _slots[x, y].onPointerExit += OnPointerExitHandler;
                }
            }

            SetOpenSlot();
        }

        private void SetOpenSlot()
        {
            for (int y = 0; y < _inventorySize.y; y++)
            {
                for (int x = 0; x < _inventorySize.x; x++)
                {
                    _slots[x, y].SetEnableSlot(_openSlot.Contains(new Vector2Int(x, y)));
                }
            }
        }

        public void DisableInventory()
        {
            _exsistingChipset.ForEach(chipset =>
            {
                chipset.onSelectChipset -= OnPointerDownChipset;
                chipset.onPointerUpChipset -= OnPointerUpChipset;
            });
            _assignedChipsets.Keys.ToList().ForEach(chipsetIndex => 
                    ChipsetManager.Instance.GetChipset(chipsetIndex).SetActive(false));

            _exsistingChipset.Clear();
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
                Chipset chipset = ChipsetManager.Instance.GetChipset(chipsetIndex);
                chipset.SetActive(true);

                Vector2Int center = _assignedChipsets[chipsetIndex].center;
                chipset.SetPosition(TransformSlotPositionToCanvasPosition(center));
            });
        }

        #endregion

        #region ChipsetEvents

        public void OnPointerDownChipset(int chipsetIndex)
        {
            _selectedChipsetIndex = chipsetIndex;

            if (chipsetIndex != -1 && _assignedChipsets.ContainsKey(chipsetIndex))
            {
                Chipset chipset = ChipsetManager.Instance.GetChipset(chipsetIndex);
                chipset.info.chipsetSize.ForEach(offset =>
                {
                    Vector2Int position = offset + _assignedChipsets[chipsetIndex].center;
                    _chipsets[position.x, position.y] = null;
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

                _selections.ForEach(selected => _slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
                _selections.Clear();
            }

            _selectedChipsetIndex = -1;
        }

        #endregion

        #region SlotEvents

        private void OnPointerEnterHandler(Vector2Int position)
        {
            _selectedSlot = _slots[position.x, position.y];
            if (_selectedChipsetIndex == null)
            {
                _selectedSlot.SetSelection(true);
            }
            else
            {
                _canInsertChipset = GetPositionSelections(position, out _selections);
                _selections.ForEach(selected => _slots[selected.x, selected.y].SetChipsetSlotState(true, _canInsertChipset));
            }
        }

        private void OnPointerExitHandler(Vector2Int position)
        {
            if (_selectedChipsetIndex == null)
            {
                _slots[position.x, position.y].SetSelection(false);
            }
            else
            {
                _selections.ForEach(selected => _slots[selected.x, selected.y].SetChipsetSlotState(false, _canInsertChipset));
                _selections.Clear();
            }
            _selectedSlot = null;
        }

        #endregion

        #region Inventory Functions

        public void AssignChipsetToInventory(Chipset chipset)
        {
            if(_exsistingChipset.Contains(chipset))
            {
                chipset.SetActive(true);
                return;
            }
            chipset.SetActive(true);
            chipset.onSelectChipset += OnPointerDownChipset;
            chipset.onPointerUpChipset += OnPointerUpChipset;
            _exsistingChipset.Add(chipset);
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
            Chipset chipset = ChipsetManager.Instance.GetChipset(chipsetIndex);

            onInsertChipset?.Invoke();
            chipset.GetOffsets().ForEach(offset =>
            {
                Vector2Int position = selectPosition + offset;
                _chipsets[position.x, position.y] = chipset;
            });

            if (_assignedChipsets.ContainsKey(chipsetIndex))
                _assignedChipsets.Remove(chipsetIndex);

            Vector2Int center = selectPosition - chipset.GetSelectOffset();
            _assignedChipsets.Add(chipsetIndex, (center, chipset.Rotation));

            _selectedChipsetIndex = chipsetIndex;
            chipset.SetPosition(GetCenterPostion(selectPosition));
            _selectedChipsetIndex = -1;
        }


        public void RemoveChipset(int chipsetindex)
        {
            if (_assignedChipsets.ContainsKey(chipsetindex))
                _assignedChipsets.Remove(chipsetindex);
        }

        #endregion

        #region Save&Load

        public List<ChipsetSave> GetInventoryData()
        {
            List<ChipsetSave> inventoryData = new List<ChipsetSave>();

            _assignedChipsets.Keys.ToList().ForEach(chipsetIndex =>
            {
                Chipset chipset = ChipsetManager.Instance.GetChipset(chipsetIndex);

                ChipsetSave chipsetData = new ChipsetSave();
                chipsetData.chipsetindex = chipset.info.id;
                chipsetData.center = _assignedChipsets[chipsetIndex].center;
                chipsetData.rotate = _assignedChipsets[chipsetIndex].rotate;

                inventoryData.Add(chipsetData);
            });

            return inventoryData;
        }

        public void SetInventoryData(List<ChipsetSave> inventoryData, List<Vector2Int> openSlot)
        {
            _openSlot = openSlot;
            SetOpenSlot();

            inventoryData.ForEach(chipsetData =>
            {
                Chipset chipsetInstance = ChipsetManager.Instance.GetChipset(chipsetData.chipsetindex);
                chipsetInstance.SetRotation(chipsetData.rotate);
                chipsetInstance.onSelectChipset += OnPointerDownChipset;
                chipsetInstance.onPointerUpChipset += OnPointerUpChipset;
                _selectedChipsetIndex = chipsetData.chipsetindex;
                InsertChipset(chipsetData.center, chipsetData.chipsetindex);
            });

            _selectedChipsetIndex = -1;
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
            Vector2 canvasPosition = RectTrm.anchoredPosition + new Vector2(-RectTrm.rect.width / 2, RectTrm.rect.height / 2)
                        + _slots[slotPosition.x, slotPosition.y].RectTrm.anchoredPosition;

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

                    if (_chipsets[positions[i].x, positions[i].y] != null) isValid = false;
                    if (_slots[positions[i].x, positions[i].y].IsEnableSlot == false) isValid = false;
                }
            }

            return isValid;
        }
    }
}
