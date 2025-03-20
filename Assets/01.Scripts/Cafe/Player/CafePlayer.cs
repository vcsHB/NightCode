using System;
using UnityEngine;

namespace Cafe
{
    public class CafePlayer : CafeEntity
    {
        public event Action onInteract;

        public CafeInput input;
        public GameObject interactMark;

        private void OnEnable()
        {
            input.onInteract += OnInteract;
        }

        private void OnDisable()
        {
            input.onInteract -= OnInteract;
        }



        #region Interact

        private void OnInteract()
        {
            onInteract?.Invoke();
            onInteract = null;
            interactMark.SetActive(false);
        }

        public void AddInteract(Action action)
        {
            onInteract += action;
            interactMark.SetActive(true);
        }

        public void RemoveInteract()
        {
            onInteract = null;
            interactMark.SetActive(false);
        }


        #endregion
    }
}
