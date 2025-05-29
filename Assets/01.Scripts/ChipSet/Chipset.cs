using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Chipset
{
    public class Chipset : MonoBehaviour
    {
        public event Action<Chipset> onSelectChipset;
        public event Action onPointerUpChipset;
        public event Action onRotate;

        public ChipsetSO info;
        public Transform chipsetSlotParent;

        private CanvasGroup _canvasGroup;

        private ChipsetSlot[] _slots;
        private Dictionary<Vector2Int, Vector2> _slotPositionDic;
        private List<Vector2Int> _prevPositions;
        private Vector2Int _selectedSlotOffset;
        private Vector2 _offset;
        private bool _isDraging = false;
        private List<Vector2Int> _chipsetSlotOffset;

        public RectTransform RectTrm => transform as RectTransform;
        public Vector2 ScreenSize => new Vector2(Screen.width, Screen.height);

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _slots = new ChipsetSlot[info.chipsetSize.Count];
            _slotPositionDic = new Dictionary<Vector2Int, Vector2>();

            for (int i = 0; i < info.chipsetSize.Count; i++)
            {
                _slots[i] = chipsetSlotParent.GetChild(i).GetComponent<ChipsetSlot>();
                _slotPositionDic.Add(info.chipsetSize[i], _slots[i].RectTrm.anchoredPosition);

                _slots[i].SetPosition(info.chipsetSize[i]);
                _slots[i].onPointerDown += OnPointerDown;
                _slots[i].onPointerUp += OnPointerUp;
            }
        }

        private void Update()
        {
            if (_isDraging)
            {
                Vector2 offset = _slotPositionDic[_selectedSlotOffset] + _offset;
                RectTrm.anchoredPosition = (Vector2)Input.mousePosition - offset;

                if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    Rotate();
                }
            }
        }

        private void Rotate()
        {
            transform.Rotate(new Vector3(0, 0, -90));
            List<Vector2> positions = _slotPositionDic.Values.ToList();

            _slotPositionDic.Clear();
            _selectedSlotOffset = new Vector2Int(_selectedSlotOffset.y, -_selectedSlotOffset.x);
            _offset = new Vector2(_offset.y, -_offset.x);

            for (int i = 0; i < _slots.Length; i++)
            {
                //CW
                Vector2Int newOffset = new Vector2Int(_slots[i].SlotPosition.y, -_slots[i].SlotPosition.x);
                _slotPositionDic.Add(newOffset, new Vector2(positions[i].y, -positions[i].x));
                _slots[i].SetPosition(newOffset);
            }
            onRotate?.Invoke();
        }

        #region Events 

        public void OnPointerDown(Vector2Int position)
        {
            _offset = (Vector2)Input.mousePosition - (RectTrm.anchoredPosition + _slotPositionDic[position]);
            _selectedSlotOffset = position;
            _canvasGroup.blocksRaycasts = false;
            _isDraging = true;
            onSelectChipset?.Invoke(this);
        }

        public void OnPointerUp(Vector2Int position)
        {
            onPointerUpChipset?.Invoke();
            _canvasGroup.blocksRaycasts = true;
            _isDraging = false;
        }

        #endregion

        public Vector2Int GetSelectOffset() => _selectedSlotOffset;


        public List<Vector2Int> GetOffsets()
        {
            List<Vector2Int> offsets = new List<Vector2Int>();

            for (int i = 0; i < _slots.Length; i++)
            {
                Vector2Int offset = _slots[i].SlotPosition - _selectedSlotOffset;
                offsets.Add(offset);
            }

            return offsets;
        }

        public void SetPosition(Vector2 anchoredPosition)
            => RectTrm.anchoredPosition = anchoredPosition + (ScreenSize / 2);

        public void SetPrevPosition(List<Vector2Int> prev) => _prevPositions = prev;

        public List<Vector2Int> GetPrevPositions() => _prevPositions;
    }
}
