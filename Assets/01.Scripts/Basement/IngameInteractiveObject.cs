using UnityEngine;

namespace Basement
{
    public abstract class IngameInteractiveObject : MonoBehaviour
    {
        private bool isMouseDown = false;

        public void MouseEvent(bool isDown)
        {
            if (isDown) OnMouseLeftButtonDown();
            else OnMouseLeftButtonUp();
        }

        protected virtual void OnMouseLeftButtonDown()
        {
            isMouseDown = true;
        }

        protected virtual void OnMouseLeftButtonUp()
        {
            isMouseDown = false;
        }

        protected virtual void OnDrag(Vector2 mousePosition)
        {

        }
    }
}
