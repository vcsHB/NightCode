using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Office
{
    [CreateAssetMenu(menuName = "SO/Basement/BasementAction")]
    public class OfficeInput : ScriptableObject, Controls.IBasementActions
    {
        public event Action<bool> onPressLeftclick;
        private Controls _controls;
        public Vector2 Direction { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseClickpoint { get; private set; }


        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Basement.SetCallbacks(this);
            }
            _controls.Basement.Enable();
        }

        private void OnDisable()
        {
            _controls.Basement.Disable();
        }

        public void OnMouseDown(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MouseClickpoint = MousePosition;
                onPressLeftclick?.Invoke(true);
            }
            if (context.canceled) onPressLeftclick?.Invoke(false);
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
        }
    }
}
