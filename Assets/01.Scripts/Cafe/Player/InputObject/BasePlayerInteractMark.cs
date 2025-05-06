using System;
using UnityEngine;

namespace Base
{
    public class BasePlayerInteractMark : MonoBehaviour
    {
        public BaseInput input;
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
