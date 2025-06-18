using Base.Office;
using UnityEngine;
namespace InteractSystem
{


    public class OfficePlayerInteractController : InteractController
    {
        protected OfficePlayer _ownerPlayer;

        private void Awake()
        {
            _ownerPlayer = GetComponent<OfficePlayer>();
            _ownerPlayer.baseInput.onInteract += HandleInteract;

        }

        private void OnDestroy()
        {
            _ownerPlayer.baseInput.onInteract -= HandleInteract;

        }
        private void HandleInteract()
        {
            TryInteract();
        }

    }
}