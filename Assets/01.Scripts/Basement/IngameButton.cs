using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class IngameButton : MonoBehaviour, IWindowPanel
    {
        public UnityEvent OnClickEvent;

        private bool _isMouseDown = false;


        #region MouseEvents

        private void OnMouseEnter()
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        private void OnMouseExit()
        {
            transform.localScale = Vector3.one;
        }

        private void OnMouseDown()
        {
            _isMouseDown = true;
        }

        private void OnMouseUp()
        {
            if (_isMouseDown)
                OnClickEvent?.Invoke();

            _isMouseDown = false;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }

}
