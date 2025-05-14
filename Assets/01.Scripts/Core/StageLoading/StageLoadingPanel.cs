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
        [SerializeField] private float _sceneChangeIntervalDuration = 2f;
        private CanvasGroup _canvasGruop;
        private Tween _toggleTween;


        public void Open()
        {
            if(_canvasGruop == null)
                _canvasGruop = GetComponent<CanvasGroup>();

            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _canvasGruop.blocksRaycasts = true;
            _canvasGruop.interactable = true;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_toggleTween = _canvasGruop.DOFade(1f, 0.2f));
            sequence.AppendInterval(_sceneChangeIntervalDuration);
            sequence.OnComplete(() => onCompleteOpenPanel?.Invoke());
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
