using UnityEngine;
namespace InteractSystem
{
    public interface IInteractable
    {
        public void DetectEnter();
        public void DetectExit();
        
        public void Interact(InteractData data);
    }
}