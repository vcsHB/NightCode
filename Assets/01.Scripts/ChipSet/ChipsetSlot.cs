using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2Int> onPointerEnter;
        public event Action<Vector2Int> onPointerExit;
        public event Action<Vector2Int> onPointerUp;
        public event Action<PointerEventData, Vector2Int> onPointerDown;

        protected Image _image;
        protected CanvasGroup _canvasGroup;
        protected Vector2Int _slotPosition;

        public Vector2Int SlotPosition => _slotPosition;
        public RectTransform RectTrm => transform as RectTransform;

        public virtual void SetPosition(Vector2Int position)
        {
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _slotPosition = position;
        }


        #region EventRegion

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(_slotPosition);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke(_slotPosition);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(eventData, _slotPosition);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(_slotPosition);
        }

        #endregion
    }
}
