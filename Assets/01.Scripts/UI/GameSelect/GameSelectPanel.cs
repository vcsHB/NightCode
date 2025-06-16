using DG.Tweening;
using UnityEngine;

namespace UI.GameSelectScene
{
    public class GameSelectPanel : MonoBehaviour, IWindowPanel
    {
        private CanvasGroup _canvasGroup;
        private Tween _openTween;

        private float _duration = 0.3f;

        public void Close()
        {
            if(_openTween != null && _openTween.active) _openTween.Kill();
            _openTween = _canvasGroup.DOFade(0f, _duration);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
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
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
