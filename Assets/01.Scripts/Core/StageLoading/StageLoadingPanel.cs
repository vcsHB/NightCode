using DG.Tweening;
using System;
using UI;
using UnityEngine;

namespace Core.StageController
{
    public class StageLoadingPanel : MonoBehaviour, IWindowPanel
    {
        public event Action onCompleteOpenPanel;
        public event Action onCompletClosePanel;

        private CanvasGroup _canvasGruop;
        private Tween _toggleTween;

        private void Awake()
        {
            _canvasGruop = GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _canvasGruop.blocksRaycasts = true;
            _canvasGruop.interactable = true;

            Debug.Log("¤±¤¤¤·¤©");
            _toggleTween = _canvasGruop.DOFade(1f, 0.2f)
                .OnComplete(() => onCompleteOpenPanel?.Invoke());
        }

        public void Close()
        {
            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _toggleTween = _canvasGruop.DOFade(0f, 0.2f)
                .OnComplete(() =>
                {
                    _canvasGruop.blocksRaycasts = false;
                    _canvasGruop.interactable = false;
                    onCompletClosePanel?.Invoke();
                });
        }
    }
}
