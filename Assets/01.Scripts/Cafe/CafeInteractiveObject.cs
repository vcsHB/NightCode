using System;
using UnityEngine;

namespace Cafe
{
    public class CafeInteractiveObject : MonoBehaviour
    {
        public CafePlayerInputObject interactObject;
        protected CafePlayer _player;


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (interactObject == null) return;

            if (collision.TryGetComponent(out _player))
            {
                _player.AddInteract(interactObject);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (interactObject == null) return;

            if (collision.TryGetComponent(out _player))
            {
                _player.RemoveInteract();
            }
        }
    }
}
