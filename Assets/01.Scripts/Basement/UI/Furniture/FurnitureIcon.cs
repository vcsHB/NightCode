using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Basement
{
    public class FurnitureIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
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

        private void ClickIcon()
        {

        }

        #region InputRegion

        public void OnPointerClick(PointerEventData eventData)
        {
            ClickIcon();
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
