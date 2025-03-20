using System;
using UnityEngine;

namespace Cafe
{
    public abstract class CafeInteractiveObject : MonoBehaviour
    {
        public abstract void OnInteract();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out CafePlayer player))
            {
                player.AddInteract(OnInteract);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CafePlayer player))
            {
                player.RemoveInteract();
            }
        }
    }
}
