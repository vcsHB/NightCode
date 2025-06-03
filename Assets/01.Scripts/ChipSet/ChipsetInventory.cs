using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Properties;
using UnityEngine;

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

        [SerializeField] private List<Vector2Int> _openSlot;
        [SerializeField] private List<Chipset> _exsistingChipset;

        private Dictionary<Chipset, List<Vector2Int>> _assignedChipsets = new Dictionary<Chipset, List<Vector2Int>>();
        private ChipsetInventorySlot[,] _slots;
        private Chipset[,] _chipsets;
        private Chipset _selectedChipset;
        private ChipsetInventorySlot _selectedSlot;

        private bool _canInsertChipset;
        private List<Vector2Int> _selections;

        public RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            //Init();
        }

        #region Initialize

        public void Init()
        {
            InitSlot(_openSlot);
            InitChipset();
        }

        private void InitSlot(List<Vector2Int> opened)
        {
            _slots = new ChipsetInventorySlot[_inventorySize.x, _inventorySize.y];
            _chipsets = new Chipset[_inventorySize.x, _inventorySize.y];

            for (int y = 0; y < _inventorySize.y; y++)
            {
                for (int x = 0; x < _inventorySize.x; x++)
                {
                    ChipsetInventorySlot slotObj = Instantiate(_slotPrefab, _slotParent);
                    Vector2Int position = new Vector2Int(x, y);
                    slotObj.name = $"Slot_{x}_{y}";
                    _slots[x, y] = slotObj.GetComponent<ChipsetInventorySlot>();

                    _slots[x, y].SetPosition(position);
                    _slots[x, y].SetEnableSlot(_openSlot.Contains(position));
                    _slots[x, y].onPointerEnter += OnPointerEnterHandler;
                    _slots[x, y].onPointerExit += OnPointerExitHandler;
                }
            }
        }

        private void InitChipset()
        {
            _exsistingChipset.ForEach(chipset =>
            {
                chipset.onSelectChipset += SelectChipset;
                chipset.onPointerUpChipset += UnSelectChipset;
            });
        }

        public void AddAssignedChipset(Chipset chipset)
        {
            _exsistingChipset.Add(chipset);
        }

        #endregion

        #region ChipsetEvents

        public void SelectChipset(Chipset chipset)
        {
            if (_assignedChipsets.ContainsKey(chipset))
            {
                _assignedChipsets[chipset].ForEach(position => _chipsets[position.x, position.y] = null);
                chipset.SetPrevPosition(_assignedChipsets[chipset]);

                _assignedChipsets.Remove(chipset);
            }
            _selectedChipset = chipset;
        }

        public void UnSelectChipset()
        {
            if (_selectedSlot == null)
            {
                ReturnChipsetToPrevPosition();
            }
            else
            {
                if (_canInsertChipset)
                {
                    InsertChipset(_selections, _selectedSlot.SlotPosition, _selectedChipset);
                }
                else
                {
                    ReturnChipsetToPrevPosition();
                }

                OnPointerExitHandler(_selectedSlot.SlotPosition);
            }

            _selectedChipset = null;
        }

        #endregion

        #region SlotEvents

        private void OnPointerEnterHandler(Vector2Int position)
        {
            _selectedSlot = _slots[position.x, position.y];
            if (_selectedChipset == null)
            {
                _selectedSlot.SetSelection(true);
            }
            else
            {
                _canInsertChipset = GetPositionSelections(position, out _selections);
                _selections.ForEach(selected => _slots[selected.x, selected.y].CheckChipsetInsertable(true, _canInsertChipset));
            }
        }

        private void OnPointerExitHandler(Vector2Int position)
        {
            if (_selectedChipset == null)
            {
                _slots[position.x, position.y].SetSelection(false);
            }
            else
            {
                _selections.ForEach(selected => _slots[selected.x, selected.y].CheckChipsetInsertable(false, _canInsertChipset));
                _selections.Clear();
            }
            _selectedSlot = null;
        }

        #endregion

        private void ReturnChipsetToPrevPosition()
        {
            onReturnChipset?.Invoke();
            List<Vector2Int> prevPositions = _selectedChipset.GetPrevPositions();

            if (prevPositions != null)
            {
                Vector2Int selectionPosition = prevPositions[0] + _selectedChipset.GetSelectOffset();
                selectionPosition -= _selectedChipset.info.chipsetSize[0];

                InsertChipset(prevPositions, selectionPosition, _selectedChipset);
            }
        }

        private void InsertChipset(List<Vector2Int> positions, Vector2Int selectedPosition, Chipset chipset)
        {
            onInsertChipset?.Invoke();
            positions.ForEach(prev => _chipsets[prev.x, prev.y] = chipset);
            _assignedChipsets.Add(chipset, positions.ToList());

            //chipset position change
            chipset.SetPosition(GetCenterPostion(selectedPosition));
        }

        private Vector2 GetCenterPostion(Vector2Int selectedPosition)
        {
            Vector2Int center = selectedPosition - _selectedChipset.GetSelectOffset();
            Vector2 centerPosition = TransformSlotPositionToCanvasPosition(center);

            return centerPosition;
        }

        private Vector2 TransformSlotPositionToCanvasPosition(Vector2Int slotPosition)
        {
            Vector2 canvasPosition = RectTrm.anchoredPosition + new Vector2(-RectTrm.rect.width / 2, RectTrm.rect.height / 2)
                        + _slots[slotPosition.x, slotPosition.y].RectTrm.anchoredPosition;

            return canvasPosition;
        }

        private bool GetPositionSelections(Vector2Int originPosition, out List<Vector2Int> positions)
        {
            positions = new List<Vector2Int>();

            if (_selectedChipset == null)
            {
                positions.Add(originPosition);
                return true;
            }

            Vector2Int offset = Vector2Int.zero;
            bool isValid = true;
            positions = _selectedChipset.GetOffsets();

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
