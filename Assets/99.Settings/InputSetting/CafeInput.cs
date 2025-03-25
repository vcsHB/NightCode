using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CafeInputInput")]
    public class CafeInput : ScriptableObject, Controls.ICafeActions
    {
        public event Action onInteract;
        public event Action<bool> onLeftClick;
        public event Action onRightClick;

        public Vector2 MoveDir { get; private set; }

        private bool _isMoveEnable = true;
        private Controls _controls;


        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Cafe.SetCallbacks(this);
            }
            _isMoveEnable = true;
            _controls.Cafe.Enable();
        }

        private void OnDisable()
        {
            _controls.Cafe.Disable();
        }


        public void EnableInput()
        {
            _isMoveEnable = true;
        }

        public void DisableInput()
        {
            _isMoveEnable = false;
        }


        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                onInteract?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_isMoveEnable == false)
            {
                MoveDir = Vector2.zero;
                return;
            }
            MoveDir = context.ReadValue<Vector2>();
        }

        public void OnMouseLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                onLeftClick?.Invoke(true);
            if (context.canceled)
                onLeftClick?.Invoke(false);
        }

        public void OnMouseRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                onRightClick?.Invoke();
        }
    }
}
