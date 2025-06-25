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
        [SerializeField] private ChipsetInventorySlot _slotPrefab;
        [SerializeField] private Transform _slotParent;
        [SerializeField] private Transform _chipsetParent;

        private ChipsetData _chipsetData;

        private Dictionary<int, (Vector2Int center, int rotate)> _assignedChipsets = new();
        private ChipsetInventorySlot[,] _slots;
        private Chipset[,] _chipsets;
        private CharacterEnum _character;

        private int _selectedChipsetIndex;
        private ChipsetInventorySlot _selectedSlot;

        private bool _canInsertChipset;
        private List<Vector2Int> _selections;

        private List<Vector2Int> _openSlot;
        private GridLayoutGroup _layoutGroup;

        #region Property

        public CharacterEnum Character => _character;
        public int SelectedChipsetIndex => _selectedChipsetIndex;
        public Chipset SelectedChipset => _chipsetData.containChipsetInstance[_selectedChipsetIndex];
        public RectTransform RectTrm => transform as RectTransform;

        #endregion


        #region Initialize

        public void Initialize(CharacterEnum character, ChipsetData chipsetData, List<Vector2Int> openedInventory)
        {
            _character = character;
            _openSlot = openedInventory;
            _chipsetData = chipsetData;
            _chipsets = new Chipset[_inventorySize.x, _inventorySize.y];
            InitSlot(_openSlot);
            SetInventoryData(chipsetData, openedInventory);
            DisableInventory();
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

            UpdateOpenSlot();
        }

        private void UpdateOpenSlot()
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
            _chipsetData.containChipsetInstance.ForEach(chipset =>
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
                Chipset chipset = _chipsetData.containChipsetInstance[chipsetIndex];
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
                Chipset chipset = _chipsetData.containChipsetInstance[chipsetIndex];
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
            if (_selectedChipsetIndex == -1)
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
            if (_selectedChipsetIndex == -1)
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
            chipset.onSelectChipset += OnPointerDownChipset;
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
            Chipset chipset = _chipsetData.containChipsetInstance[chipsetIndex];

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
            _chipsetData.AddChipset(Character, new CharacterChipsetData(chipsetIndex, center, chipset.Rotation));
        }

        public void RemoveChipset(int chipsetindex)
        {
            if (_assignedChipsets.ContainsKey(chipsetindex))
                _assignedChipsets.Remove(chipsetindex);
        }

        #endregion

        #region Save&Load

        public void SetInventoryData(ChipsetData chipsetData, List<Vector2Int> openSlot)
        {
            _openSlot = openSlot;
            UpdateOpenSlot();

            List<int> chipsetIndexList = chipsetData.GetCharacterChipsetIndex(Character);

            for (int i = 0; i < chipsetIndexList.Count; i++)
            {
                int index = chipsetIndexList[i];

                Chipset chipset = chipsetData.containChipsetInstance[index];
                CharacterChipsetData data = chipsetData.GetCharacterInventoryData(Character)[i];

                chipset.SetRotation(data.rotate);
                chipset.onSelectChipset -= OnPointerDownChipset;
                chipset.onSelectChipset += OnPointerDownChipset;
                chipset.onPointerUpChipset -= OnPointerUpChipset;
                chipset.onPointerUpChipset += OnPointerUpChipset;

                _assignedChipsets.Add(data.chipsetIndex, (data.center, data.rotate));
            }

            _selectedChipsetIndex = -1;
        }

        public List<CharacterChipsetData> GetChipsetData()
        {
            List<CharacterChipsetData> chipsetDatas = new List<CharacterChipsetData>();

            _assignedChipsets.Keys.ToList().ForEach(index =>
            {
                chipsetDatas.Add(new CharacterChipsetData(index, _assignedChipsets[index].center, _assignedChipsets[index].rotate));
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
                (Camera.main, _slots[slotPosition.x, slotPosition.y].RectTrm.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle((_chipsetParent as RectTransform), screenPosition, Camera.main, out Vector2 canvasPosition) ;

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
