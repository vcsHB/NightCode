using UnityEngine;
using UnityEngine.Events;
namespace Office.InteractSystem
{

    public class InteractionTarget : MonoBehaviour, IInteractable
    {
        public UnityEvent OnHoverEnterEvent;
        public UnityEvent OnHoverExitEvent;
        public UnityEvent OnInteractEvent;
        

        public void HoverEnter()
        {
            OnHoverEnterEvent?.Invoke();
        }

        public void HoverExit()
        {
            OnHoverExitEvent?.Invoke();
        }

        public void Interact()
        {
            OnInteractEvent?.Invoke();
        }
    }
}