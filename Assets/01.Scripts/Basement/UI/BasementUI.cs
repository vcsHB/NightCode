using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class BasementUI : MonoBehaviour
    {
        [SerializeField] private Button _goLeftButton, _goRightButton;

        private RectTransform _leftButtonTrm => _goLeftButton.transform as RectTransform;
        private RectTransform _rightButtonTrm => _goRightButton.transform as RectTransform;

        private Sequence _seq;

        public void OnChangeRoom(bool canGoLeft, bool canGoRight)
        {
            _goLeftButton.gameObject.SetActive(canGoLeft);
            _goRightButton.gameObject.SetActive(canGoRight);
        }


        public void Open()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            _seq.Append(_leftButtonTrm.DOAnchorPosX(40f, 0.2f))
                .Join(_rightButtonTrm.DOAnchorPosX(-40f, 0.2f));
        }

        public void Close()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            _seq.Append(_leftButtonTrm.DOAnchorPosX(-40f, 0.2f))
                .Join(_rightButtonTrm.DOAnchorPosX(40f, 0.2f));
        }
    }
}
