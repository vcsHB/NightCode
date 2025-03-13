using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [CreateAssetMenu(menuName = "SO/Input/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnAttackEvent;
        public event Action<bool> OnShootEvent;
        public event Action OnShootRopeEvent;
        public event Action OnRemoveRopeEvent;
        public event Action<Vector2> MovementEvent;
        public event Action JumpEvent;
        public event Action PullEvent;
        public event Action TurboEvent;
        public event Action OnCharacterChangeEvent;
        private Controls _controls;
        public Vector2 InputDirection { get; private set; }

        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseWorldPosition {get; private set; }

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
            MovementEvent?.Invoke(InputDirection);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnAttackEvent?.Invoke();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnShootEvent?.Invoke(true);
                OnShootRopeEvent?.Invoke();
            }
            else if (context.canceled)
            {
                OnShootEvent?.Invoke(false);
                OnRemoveRopeEvent?.Invoke();
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
                PullEvent?.Invoke();
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
            MouseWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);
        }

        public void OnChangeTag(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnCharacterChangeEvent?.Invoke();
            }
        }
    }

}