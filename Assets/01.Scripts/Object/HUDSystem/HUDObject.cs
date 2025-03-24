using UnityEngine;
namespace HUDSystem
{

    public class HUDObject : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public virtual void Open()
        {
            _canvasGroup.alpha = 1f;
        }

        public virtual void Close()
        {
            _canvasGroup.alpha = 0f;
        }
    }
}