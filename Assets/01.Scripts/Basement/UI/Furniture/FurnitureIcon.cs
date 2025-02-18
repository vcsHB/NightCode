using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Basement
{
    public class FurnitureIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Action<FurnitureSO> OnClick;

        private Image _image;
        private FurnitureSO _furniture;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetFurniture(FurnitureSO furniture)
        {
            _furniture = furniture;
            _image.sprite = _furniture.icon;
        }

        #region InputRegion

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_furniture);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        #endregion
    }
}
