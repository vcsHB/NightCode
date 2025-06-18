using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace InteractSystem
{

    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteractionEvent;
        public UnityEvent OnInteractionEnterEvent;
        public UnityEvent OnInteractionExitEvent;

        public void DetectEnter()
        {
            OnInteractionEnterEvent?.Invoke();
        }

        public void DetectExit()
        {
            OnInteractionExitEvent?.Invoke();
        }

        public void Interact(InteractData data)
        {
            OnInteractionEvent?.Invoke();
        }
    }
}