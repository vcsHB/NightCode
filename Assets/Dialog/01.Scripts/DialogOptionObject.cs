using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogOptionObject : MonoBehaviour
    {
        internal Action onSelect;
        [SerializeField] private RectTransform _bg;
        [SerializeField] private TextMeshProUGUI _tmp;
        [SerializeField] private float _closePosition;
        private CanvasGroup _canvasGroup;
        private Vector2 _originPos;

        private Tween _toggleTween;
        private float _duration = 0.2f;

        private RectTransform rectTransform => transform as RectTransform;


        private void Awake()
        {
            _originPos = rectTransform.anchoredPosition;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetOption(Option option)
        {
            _tmp.SetText(option.optionTxt);
        }

        public void OnSelectOption()
        {
            onSelect?.Invoke();
        }

        public void OnHover(bool isHover)
        {
            _canvasGroup.alpha = isHover ? 0.9f : 0.7f;
            transform.localScale = Vector3.one * (isHover ? 1.03f : 1.0f);
        }

        public void Open()
        {
            _bg.localScale = new Vector3(0, 1, 1);
            rectTransform.anchoredPosition = _originPos;
            
            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _toggleTween = _bg.DOScaleX(1, _duration);
        }

        public void Close()
        {
            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _toggleTween = rectTransform.DOAnchorPosX(_closePosition, _duration);
        }
    }
}
