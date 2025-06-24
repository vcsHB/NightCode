using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Chipset
{
    public class Chipset : MonoBehaviour
    {
        public event Action<ChipsetSO> SetExplain;
        public event Action UnSetExplain;
        public event Action<int> onSelectChipset;
        public event Action onPointerUpChipset;
        public event Action onRotate;

        public ChipsetSO info;
        public Transform chipsetSlotParent;

        private CanvasGroup _canvasGroup;

        private Vector2Int _centerPosition;
        private int _rotation;

        private ChipsetSlot[] _slots;
        private Dictionary<Vector2Int, Vector2> _slotPositionDic;
        private Vector2Int _prevPosition;
        private int _prevRotate;

        private Vector2Int _selectedSlotOffset;
        private Vector2 _offset;

        private bool _isDraging = false;
        private bool _isForcePointerDown = false;
        private int _index;

        #region Property Field

        public int Index => _index;
        public int Rotation => _rotation;
        public bool IsForcePointerDown => _isForcePointerDown;
        public RectTransform RectTrm => transform as RectTransform;
        public RectTransform ParentRectTrm => transform.parent as RectTransform;


        #endregion


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            SlotInitialize();
        }

        private void Update()
        {
            ChipsetDrag();
            CheckForceMouseUp();
        }

        private void SlotInitialize()
        {
            _slots = new ChipsetSlot[info.chipsetSize.Count];
            _slotPositionDic = new Dictionary<Vector2Int, Vector2>();

            for (int i = 0; i < info.chipsetSize.Count; i++)
            {
                _slots[i] = chipsetSlotParent.GetChild(i).GetComponent<ChipsetSlot>();
                _slotPositionDic.Add(info.chipsetSize[i], _slots[i].RectTrm.anchoredPosition);

                _slots[i].SetPosition(info.chipsetSize[i]);
                _slots[i].onPointerDown += OnPointerDown;
                _slots[i].onPointerUp += OnPointerUp;
                _slots[i].onPointerEnter += HandleSetExplain;
                _slots[i].onPointerExit += HandleUnSetExplain;
            }
        }

        private void HandleUnSetExplain(Vector2Int position)
        {
            UnSetExplain?.Invoke();
        }

        private void HandleSetExplain(Vector2Int position)
        {
            SetExplain?.Invoke(info);
        }

        private void ChipsetDrag()
        {
            if (_isDraging)
            {
                Vector2 offset = ClockWise(_offset, _rotation);
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTrm.parent as RectTransform,
                    Mouse.current.position.value, Camera.main, out Vector2 localPosition))
                {
                    RectTrm.localPosition = localPosition - offset;
                }

                if (info.isRotatable && Keyboard.current.rKey.wasPressedThisFrame)
                {
                    Rotate();
                }
            }
        }

        #region RotateFunc

        private void Rotate()
        {
            if (++_rotation > 3) _rotation -= 4;
            transform.Rotate(new Vector3(0, 0, -90));
            onRotate?.Invoke();
        }

        private Vector2Int ClockWise(Vector2Int position, int rotateCnt)
        {
            int cnt = rotateCnt % 4;

            switch (cnt)
            {
                case 0:
                    return position;
                case 1:
                    return new Vector2Int(position.y, -position.x);
                case 2:
                    return -position;
                case 3:
                    return new Vector2Int(-position.y, position.x);
            }
            return Vector2Int.zero;
        }
        private Vector2 ClockWise(Vector2 position, int rotateCnt)
        {
            int cnt = rotateCnt % 4;

            switch (cnt)
            {
                case 0:
                    return position;
                case 1:
                    return new Vector2(position.y, -position.x);
                case 2:
                    return -position;
                case 3:
                    return new Vector2(-position.y, position.x);
            }
            return Vector2.zero;
        }

        #endregion

        #region Events 

        public void ForceOnPointerDown(Vector2Int position, PointerEventData data)
        {
            for (int i = 0; i < _slots.Length; ++i)
            {
                if (_slots[i].SlotPosition == position)
                {
                    // Spread event to chipset slot
                    ExecuteEvents.Execute(_slots[i].gameObject,
                        data, ExecuteEvents.pointerDownHandler);

                    _isForcePointerDown = true;
                }
            }
        }

        private void CheckForceMouseUp()
        {
            if (_isForcePointerDown && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                OnPointerUp(Vector2Int.zero);
                _isForcePointerDown = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData, Vector2Int position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTrm, eventData.position, eventData.pressEventCamera, out _offset);

            _selectedSlotOffset = position;
            _canvasGroup.blocksRaycasts = false;
            onSelectChipset?.Invoke(_index);
            _isDraging = true;
            Debug.Log("นึนึ?!!");
        }

        public void OnPointerUp(Vector2Int position)
        {
            onPointerUpChipset?.Invoke();
            _canvasGroup.blocksRaycasts = true;
            _isDraging = false;
            Debug.Log("นึ?!");
        }

        #endregion

        public Vector2Int GetSelectOffset() => ClockWise(_selectedSlotOffset, _rotation);

        public List<Vector2Int> GetOffsets()
        {
            List<Vector2Int> offsets = new List<Vector2Int>();

            for (int i = 0; i < _slots.Length; i++)
            {
                Vector2Int offset = _slots[i].SlotPosition - _selectedSlotOffset;
                offsets.Add(ClockWise(offset, _rotation));
            }

            return offsets;
        }

        public void SetPosition(Vector2 localPosition)
            => RectTrm.localPosition = localPosition;

        public void SetPrevPosition(Vector2Int center, int rotate)
        {
            //_selectedSlotOffset = Vector2Int.zero;
            _prevPosition = center;
            _prevRotate = rotate;
        }

        public Vector2Int GetPrevPosition()
        {
            _selectedSlotOffset = Vector2Int.zero;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 * _prevRotate));
            _rotation = _prevRotate;
            return _prevPosition;
        }

        public void SetActive(bool isEnable)
        {
            if (isEnable) transform.localScale = Vector3.one;
            else transform.localScale = Vector3.zero;
        }

        public void SetRotation(int rotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 * rotation));
            _rotation = rotation;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
    }
}
