using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Cafe
{
    public class StartPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private Image _bg;

        private Sequence _openCloseSeq;

        private void Awake()
        {
            Open();
        }

        public void Open()
        {
            if (_openCloseSeq != null && _openCloseSeq.active)
                _openCloseSeq.Kill();

            _bg.color = new Color(_bg.color.r, _bg.color.g, _bg.color.b, 1);

            _openCloseSeq = DOTween.Sequence();
            _openCloseSeq.AppendInterval(0.2f)
                .Append(_bg.DOFade(0f, 0.5f))
                .OnComplete(CafeManager.Instance.StartCafe);
     }

        public void Close()
        {
            _bg.gameObject.SetActive(false);
        }
    }
}
