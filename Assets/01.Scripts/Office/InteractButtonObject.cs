using UnityEngine;
using UnityEngine.Events;

namespace Base.Office
{
    public class InteractButtonObject : BaseInteractiveObject
    {
        public UnityEvent<BasePlayer> onInteract;

        public override void OnPlayerInteract()
        {
            _player.AddInteract(OnInteract);
        }

        public override void OnPlayerInteractExit()
        {
            _player.RemoveInteract(OnInteract);
        }

        private void OnInteract()
        {
            onInteract?.Invoke(_player);
        }
    }
}
