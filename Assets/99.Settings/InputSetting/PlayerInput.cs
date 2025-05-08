using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [System.Serializable]
    public struct PlayerInputStatus
    {
        public bool attack;
        public bool shoot;
        public bool move;
        public bool jump;
        public bool change;
        public bool turbo;
        public bool use;

        public void SetEnableAll()
        {
            attack = true;
            shoot = true;
            move = true;
            jump = true;
            change = true;
            turbo = true;
            use = false;
        }
    }
    [CreateAssetMenu(menuName = "SO/Input/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public PlayerInputStatus playerInputStatus;
        public event Action OnAttackEvent;
        public event Action<bool> OnShootEvent;
        public event Action OnShootRopeEvent;
        public event Action OnUseEvent;
        public event Action OnUseCancelEvent;
        public event Action OnRemoveRopeEvent;
        public event Action<Vector2> MovementEvent;
        public event Action JumpEvent;
        public event Action PullEvent;
        public event Action TurboEvent;
        public event Action OnCharacterChangeEvent;
        private Controls _controls;
        [SerializeField] private Vector2 _inputDirection;
        public Vector2 InputDirection
        {
            get
            {
                if (!playerInputStatus.move) return Vector2.zero;
                return _inputDirection;
            }
            private set { }
        }

        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseWorldPosition { get; private set; }
        public bool IsShootRelease { get; private set; }

        #region InputSwitchs


        #endregion

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            MousePosition = Vector2.zero;
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.move)
            {
                _inputDirection = Vector2.zero;
                return;
            }
            _inputDirection = context.ReadValue<Vector2>();
            MovementEvent?.Invoke(_inputDirection);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.shoot) return;
            if (context.performed)
            {
                IsShootRelease = false;
                OnShootEvent?.Invoke(true);
                OnShootRopeEvent?.Invoke();
            }
            else if (context.canceled)
            {
                IsShootRelease = true;
                OnShootEvent?.Invoke(false);
                OnRemoveRopeEvent?.Invoke();
            }
        }
        public void OnUse(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.use) return;

            if (context.performed)
            {
                OnUseEvent?.Invoke();
            }
            else if (context.canceled)
            {
                OnUseCancelEvent?.Invoke();
            }
        }


        public void OnTurbo(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.turbo) return;
            if (context.performed)
            {
                TurboEvent?.Invoke();
            }

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.jump) return;
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
            if (!playerInputStatus.change) return;
            if (context.performed)
            {
                OnCharacterChangeEvent?.Invoke();
            }
        }

        public void SetEnabledAllStatus()
        {
            playerInputStatus.SetEnableAll();
        }

        public void SetInputStatus(PlayerInputStatus inputStatus)
        {
            playerInputStatus = inputStatus;
            if (!inputStatus.move)
                InputDirection = Vector2.zero;

        }

    }

}