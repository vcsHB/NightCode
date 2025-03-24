using InputManage;
using UnityEngine;
namespace UI.Common
{

    public abstract class KeyBindPanelController : MonoBehaviour
    {
        [SerializeField] protected UIInputReader _uiInput;
        protected IWindowPanel _window;
        [SerializeField] protected bool _canControl;
        protected virtual void Awake()
        {
            _window = GetComponent<IWindowPanel>();
        }


        public virtual void SetEnableControl() => _canControl = true;
        public virtual void SetDisableControl() => _canControl = false;
        public void SetControl(bool value) => _canControl = value;

    }
}