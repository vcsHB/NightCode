using Agents.Players;
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
            //To subscribe event to BasePlayer
            if (collision.TryGetComponent(out BasePlayer player))
            {
                if (_player == null) _player = player;
                OnPlayerInteract();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BasePlayer player))
            {
                if (_player == null) _player = player;
                OnPlayerInteractExit();
            }
        }
    }
}
