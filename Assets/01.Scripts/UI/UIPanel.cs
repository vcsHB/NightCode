using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace UI
{

    public class UIPanel : MonoBehaviour, IWindowPanel
    {
        protected CanvasGroup _canvasGroup;
        public UnityEvent OnOpenEvent;
        public UnityEvent OnCloseEvent;
        [SerializeField] protected bool _useUnscaledTime;
        [SerializeField] protected float _activeDuration = 1f;
        [SerializeField] protected bool _isActive;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Open()
        {
            SetCanvasActive(true);
            OnOpenEvent?.Invoke();
        }

        public virtual void Close()
        {
            SetCanvasActive(false);
            OnCloseEvent?.Invoke();
        }

        public void SetCanvasActive(bool value)
        {
            _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration).SetUpdate(_useUnscaledTime).OnComplete(() => _isActive = value);
            SetInteractable(value);
        }

        public void SetCanvasActiveImmediately(bool value)
        {
            _canvasGroup.alpha = value ? 1f : 0f;
            SetInteractable(value);
        }

        protected void SetInteractable(bool value)
        {
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}