
using UnityEngine;

namespace UI.Common
{

    public class EscToggler : KeyBindPanelController
    {
        private IWindowTogglable _toggleTarget;
        protected override void Awake()
        {
            base.Awake();
            _toggleTarget = GetComponent<IWindowTogglable>();

            if (_canControl)
                _uiInput.OnEscEvent += HandleTogglePanel;

        }

        private void OnDestroy()
        {
            if (_canControl)
                _uiInput.OnEscEvent -= HandleTogglePanel;
        }

        public override void SetDisableControl()
        {
            base.SetDisableControl();
            _uiInput.OnEscEvent -= HandleTogglePanel;

        }

        public override void SetEnableControl()
        {
            base.SetEnableControl();
            _uiInput.OnEscEvent += HandleTogglePanel;

        }

        private void HandleTogglePanel()
        {
            if (!_canControl) return;
            _toggleTarget.Toggle();
        }
    }
}