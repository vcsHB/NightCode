using UnityEngine;
using UnityEngine.EventSystems;

namespace Basement
{
    public class BuildUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public int floor;
        public int roomNumber;
        [SerializeField] private BasementSO _basementSO;

        private RectTransform _rectTrm => transform as RectTransform;

        public void OnPointerClick(PointerEventData eventData)
        {
            //_basementSO.
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTrm.localScale = Vector3.one * 1.05f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rectTrm.localScale = Vector3.one;
        }
    }
}
