using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI.GameSelectScene
{
    public class GameSelectPanel : MonoBehaviour, IWindowPanel, IWindowTogglable
    {
        public UnityEvent onClose;
        public UnityEvent OnOpenEvent;

        private CanvasGroup _canvasGroup;
        private Tween _openTween;
        private bool _isActive;

        private float _duration = 0.3f;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public void Close()
        {
            if (_openTween != null && _openTween.active) _openTween.Kill();
            _openTween = _canvasGroup.DOFade(0f, _duration).OnComplete(() => onClose?.Invoke());
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _isActive = false;
        }

        public void Open()
        {
            if (_openTween != null && _openTween.active) _openTween.Kill();
            _openTween = _canvasGroup.DOFade(1f, _duration)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                });
            _isActive = true;
            OnOpenEvent?.Invoke();
        }

        public void Toggle()
        {
            if (_isActive)
                Close();
            else
                Open();
        }

    }
}
