using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [CreateAssetMenu(menuName ="SO/Input/UIInputReader")]
    public class UIInputReader : ScriptableObject, Controls.IUIActions
    {
        public event Action OnEscEvent;

        private Controls _controls;

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

        public void OnOnEsc(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnEscEvent?.Invoke();
        }


    }

}