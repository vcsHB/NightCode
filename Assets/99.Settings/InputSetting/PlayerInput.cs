using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [CreateAssetMenu(menuName = "SO/Input/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public event Action<bool> ShootEvent;
        public event Action JumpEvent;
        public event Action TurboEvent;

        private Controls _controls;

        public Vector2 InputDirection { get; private set; }
        public Vector2 MousePosition { get; private set; }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ShootEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                ShootEvent?.Invoke(false);
            }
        }

        public void OnTurbo(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                TurboEvent?.Invoke();
            }

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpEvent?.Invoke();
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }
    }

}