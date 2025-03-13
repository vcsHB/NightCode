using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public class BasementCommonUI : BasementUI
    {
        [Space]
        public CanvasGroup canvasGroup;
        public OpenCloseAnimType animType;
        public bool closeOnStart;

        public float tweenStartValue, tweenEndValue;
        public Vector2 tweenStartPos, tweenEndPos;
        public float tweenDuration;

        private Tween _moveTween;

        public RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            if (closeOnStart == false) return;
            switch (animType)
            {
                case OpenCloseAnimType.SetActive:
                    {
                        gameObject.SetActive(false);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenX:
                    {
                        RectTrm.anchoredPosition = new Vector2(tweenStartValue, RectTrm.anchoredPosition.y);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenY:
                    {
                        RectTrm.anchoredPosition = new Vector2(RectTrm.anchoredPosition.x, tweenStartValue);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenXY:
                    {
                        RectTrm.anchoredPosition = tweenStartPos;
                        break;
                    }
                case OpenCloseAnimType.Fade:
                    {
                        canvasGroup.alpha = tweenStartValue;
                        break;
                    }
            }
        }

        protected override void OpenAnimation()
        {
            switch (animType)
            {
                case OpenCloseAnimType.SetActive:
                    {
                        gameObject.SetActive(true);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenX:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPosX(tweenEndValue, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenY:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPosY(tweenEndValue, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenXY:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPos(tweenEndPos, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.Fade:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = canvasGroup.DOFade(1, tweenDuration);
                        break;
                    }
            }
        }

        protected override void CloseAnimation()
        {
            switch (animType)
            {
                case OpenCloseAnimType.SetActive:
                    {
                        gameObject.SetActive(false);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenX:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPosX(tweenStartValue, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenY:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPosY(tweenStartValue, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.MoveTweenXY:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = RectTrm.DOAnchorPos(tweenStartPos, tweenDuration);
                        break;
                    }
                case OpenCloseAnimType.Fade:
                    {
                        if (_moveTween != null && _moveTween.active)
                            _moveTween.Kill();

                        _moveTween = canvasGroup.DOFade(0, tweenDuration);
                        break;
                    }
            }
        }

        public enum OpenCloseAnimType
        {
            SetActive,
            MoveTweenX,
            MoveTweenY,
            MoveTweenXY,
            Fade
        }
    }
}
