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

        public virtual void DetectEnter()
        {
            OnInteractionEnterEvent?.Invoke();
        }

        public virtual void DetectExit()
        {
            OnInteractionExitEvent?.Invoke();
        }

        public virtual void Interact(InteractData data)
        {
            OnInteractionEvent?.Invoke();
        }
    }
}