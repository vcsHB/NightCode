using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragableUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        protected Vector2 _offset;
        protected Vector2 _localPosition;

        protected RectTransform RectTrm => transform as RectTransform;

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTrm.parent as RectTransform, eventData.position, eventData.pressEventCamera, out _localPosition))
            {
                RectTrm.localPosition = _localPosition - _offset;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTrm, eventData.position, eventData.pressEventCamera, out _offset);
        }

        public virtual void OnPointerUp(PointerEventData eventData) { }
    }
}
