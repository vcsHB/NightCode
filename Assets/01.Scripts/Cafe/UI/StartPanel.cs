using DG.Tweening;
using UI;
using UnityEngine;

namespace Cafe
{
    public class StartPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private RectTransform _readyText;
        [SerializeField] private RectTransform _startText;

        private Sequence _openCloseSeq;
        private RectTransform rectTrm => transform as RectTransform;

        private void Awake()
        {
            Open();
        }

        public void Open()
        {
            if (_openCloseSeq != null && _openCloseSeq.active)
                _openCloseSeq.Kill();

            _openCloseSeq = DOTween.Sequence();
            _openCloseSeq.AppendInterval(0.2f)
                .Append(_readyText.DOScale(1, 0.2f))
                .AppendInterval(0.5f)
                .Append(_readyText.DOScale(0, 0.2f))
                .Append(_startText.DOScale(1, 0.2f))
                .AppendInterval(1f)
                .Append(_startText.DOScale(0, 0.3f))
                .OnComplete(CafeManager.Instance.StartCafe);
        }

        public void Close()
        {
            if (_openCloseSeq != null && _openCloseSeq.active)
                _openCloseSeq.Kill();

            _openCloseSeq = DOTween.Sequence();
            _openCloseSeq.Append(rectTrm.DOAnchorPosY(1080, 0.2f));
        }
    }
}
