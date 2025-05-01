using UnityEngine;
namespace UI.Common
{

    public class EscCloser : KeyBindPanelController
    {

        protected override void Awake()
        {
            base.Awake();
            if (_canControl)
                _uiInput.OnEscEvent += HandleClosePanel;
        }
        private void OnDestroy()
        {
            if (_canControl)
                _uiInput.OnEscEvent -= HandleClosePanel;
        }

        public override void SetDisableControl()
        {
            base.SetDisableControl();
            _uiInput.OnEscEvent -= HandleClosePanel;

        }

        public override void SetEnableControl()
        {
            base.SetEnableControl();
            _uiInput.OnEscEvent += HandleClosePanel;

        }

        private void HandleClosePanel()
        {
            if (!_canControl) return;
            _window.Close();
        }
    }
}