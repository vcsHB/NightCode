using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.NodeViewScene.StageSelectionUIs
{
    public class SelectButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClickEvent;

        public RectTransform RectTrm => transform as RectTransform;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTrm.localScale = Vector3.one * 1.05f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTrm.localScale = Vector3.one;
        }
    }
}
