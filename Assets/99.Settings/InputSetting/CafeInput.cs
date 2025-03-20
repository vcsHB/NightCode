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
        public Vector2 moveDir { get; private set; }

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
            _controls.Basement.Disable();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                onInteract?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveDir = context.ReadValue<Vector2>();
        }
    }
}
