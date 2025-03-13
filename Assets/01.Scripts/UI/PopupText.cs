using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Basement
{
    public class PopupText : MonoBehaviour
    {
        public TextMeshProUGUI tmp;
        public float lifeTime = 1f;
        public float speed = 50f;
        public RectTransform RectTrm => transform as RectTransform;

        private Sequence _seq;

        public void SetText(string text)
        {
            tmp.SetText(text);

            _seq = DOTween.Sequence();
            _seq.Append(RectTrm.DOAnchorPosY(RectTrm.anchoredPosition.y + speed, lifeTime))
                .Join(tmp.DOFade(0, lifeTime))
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
