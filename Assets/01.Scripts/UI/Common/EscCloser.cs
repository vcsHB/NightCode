using UnityEngine;
namespace UI.Common
{

    public class EscCloser : KeyBindPanelController
    {

        protected override void Awake()
        {
            base.Awake();
            _uiInput.OnEscEvent += HandleClosePanel;
        }
        private void OnDestroy()
        {
            _uiInput.OnEscEvent -= HandleClosePanel;
        }

        private void HandleClosePanel()
        {
            if(!_canControl) return;
            _window.Close();
        }


    }
}