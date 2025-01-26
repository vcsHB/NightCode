using UnityEngine;

namespace Basement
{
    public abstract class BasementObject : MonoBehaviour
    {
        public abstract void OnInteractObject();
        public abstract void OnDeInteractObject();

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            OnInteractObject();
        }

        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            OnDeInteractObject();
        }
    }
}
