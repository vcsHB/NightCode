using UnityEngine;

namespace Base.Office
{
    public abstract class OfficeInteractive : MonoBehaviour
    {
        protected bool _isActive;
        protected OfficePlayer _player;

        public abstract void OnInteract();


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out _player))
            {
                _player.AddInteract(OnInteract);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player))
            {
                _player.RemoveInteract(OnInteract);
            }
        }
    }
}
