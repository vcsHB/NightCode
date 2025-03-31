using System;
using UnityEngine;

namespace Cafe
{
    public class CafePlayerInteractMark : MonoBehaviour
    {
        public CafeInput input;
        public event Action onInteract;


        private void OnEnable()
        {
            input.onInteract += InteractAction;
        }
        private void OnDisable()
        {
            input.onInteract -= InteractAction;
        }

        private void InteractAction()
        {
            onInteract?.Invoke();
            onInteract = null;
            gameObject.SetActive(false);
        }
    }
}
