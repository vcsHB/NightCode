using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Base
{
    [CreateAssetMenu(menuName = "SO/Cafe/BaseInputAction")]
    public class BaseInput : ScriptableObject, Controls.IBaseActions
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
                _controls.Base.SetCallbacks(this);
            }
            _isMoveEnable = true;
            _controls.Base.Enable();
        }

        private void OnDisable()
        {
            _controls.Base.Disable();
        }


        public void EnableInput()
        {
            _isMoveEnable = true;
        }

        public void DisableInput()
        {
            _isMoveEnable = false;
            MoveDir = Vector2.zero;
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
