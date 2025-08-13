using UnityEngine;
namespace InteractSystem
{

    public class OnceInteractObject : InteractableObject
    {
        private bool _isUsed;


        public void ResetInteractable()
        {
            _isUsed = false;
        }

        public override void Interact(InteractData data)
        {
            if (_isUsed) return;
            base.Interact(data);
            OnInteractionExitEvent?.Invoke();
            _isUsed = true;
        }

        public override void DetectEnter()
        {
            if (_isUsed) return;
            base.DetectEnter();
        }

        public override void DetectExit()
        {
            if (_isUsed) return;
            base.DetectExit();
        }
    }
}