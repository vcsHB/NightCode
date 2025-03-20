using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CafeInputInput")]
    public class CafeInput : ScriptableObject, Controls.ICafeActions
    {
        public event Action onInteract;
        public event Action onLeftClick;
        public event Action onRightClick;

        public Vector2 MoveDir { get; private set; }

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Cafe.SetCallbacks(this);
            }
            _controls.Cafe.Enable();
        }

        private void OnDisable()
        {
            _controls.Cafe.Disable();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                onInteract?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>();
        }

        public void OnMouseLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                onLeftClick?.Invoke();
        }

        public void OnMouseRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                onRightClick?.Invoke();
        }
    }
}
