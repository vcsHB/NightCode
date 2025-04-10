using Office.InteractSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace UI.OfficeScene.Armory
{

    public class HoverInteractBlockPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {   
        [SerializeField] private InteractController _interactController;
        public UnityEvent OnHoverEnterEvent;    
        public UnityEvent OnHoverExitEvent;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnterEvent?.Invoke();
            _interactController.SetInteractable(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExitEvent?.Invoke();
            _interactController.SetInteractable(true);
        }
    }
}