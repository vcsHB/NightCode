using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [CreateAssetMenu(menuName = "SO/Input/UIInputReader")]
    public class UIInputReader : ScriptableObject, Controls.IUIActions
    {
        public event Action OnEscEvent;
        public event Action OnSpaceEvent;
        public event Action OnLeftClickEvent;
        public event Action OnRightClickEvent;

        private Controls _controls;
        public Vector2 MousePositionOnScreen { get; private set; }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.UI.SetCallbacks(this);
            }
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.UI.Disable();
        }

        public void OnEsc(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnEscEvent?.Invoke();
            }
        }

        public void OnSpace(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnSpaceEvent?.Invoke();
            }
        }

        public void OnLeftMouseButton(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnLeftClickEvent?.Invoke();

        }

        public void OnRightMouseButton(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnRightClickEvent?.Invoke();

        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            MousePositionOnScreen = context.ReadValue<Vector2>();
        }
    }

}