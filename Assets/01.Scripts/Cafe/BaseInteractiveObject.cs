using UnityEngine;

namespace Base
{
    public abstract class BaseInteractiveObject : MonoBehaviour
    {
        protected BasePlayer _player;

        public abstract void OnPlayerInteract();
        public abstract void OnPlayerInteractExit();


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player))
            {
                OnPlayerInteract();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player))
            {
                OnPlayerInteractExit();
            }
        }
    }
}
