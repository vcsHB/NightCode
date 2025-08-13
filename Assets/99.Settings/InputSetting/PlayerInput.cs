using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
            use = true;
        }

        public void SetDisableAll()
        {
            attack = false;
            shoot = false;
            move = false;
            jump = false;
            change = false;
            turbo = false;
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
        public event Action<int> OnCharacterChangeEvent;
        public event Action OnInteractEvent;
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

        public void ResetAllSubscription()
        {
            OnAttackEvent = null;
            OnShootEvent = null;
            OnShootRopeEvent = null;
            OnUseEvent = null;
            OnUseCancelEvent = null;
            OnRemoveRopeEvent = null;
            JumpEvent = null;
            PullEvent = null;
            TurboEvent = null; 
            OnCharacterChangeEvent = null;
            OnInteractEvent = null;
        }

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

        }

        public void SetEnabledAllStatus()
        {
            playerInputStatus.SetEnableAll();
        }
        public void SetDisableAllStatus()
        {
            playerInputStatus.SetDisableAll();
        }

        public void SetInputStatus(PlayerInputStatus inputStatus)
        {
            playerInputStatus = inputStatus;
            if (!inputStatus.move)
                InputDirection = Vector2.zero;

        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.use) return;
            if (context.performed)
            {
                OnInteractEvent?.Invoke();
            }

        }

        public void OnChcratcerChange(InputAction.CallbackContext context)
        {
            if (!playerInputStatus.change) return;
            var keyControl = context.control as KeyControl;
            if (keyControl == null) return;

            // Digit1 = 41, need Binding Exception
            OnCharacterChangeEvent?.Invoke((int)keyControl.keyCode - 41);
        }
    }

}