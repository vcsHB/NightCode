using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{

    public class SceneExitPanel : UIPanel
    {
        public UnityEvent OnTweenCompleteEvent;
        [SerializeField] private Image _leftFillImage;
        [SerializeField] private Image _rightFillImage;
        [SerializeField] private float _fillDuration = 2f;

        public override void Open()
        {
            base.Open();
            _leftFillImage.DOFillAmount(0.55f, _fillDuration).SetUpdate(true);
            _rightFillImage.DOFillAmount(0.55f, _fillDuration).SetUpdate(true).OnComplete(() => OnTweenCompleteEvent?.Invoke());
        }

        public override void Close()
        {
            base.Close();
            _leftFillImage.DOFillAmount(0f, _fillDuration).SetUpdate(true);
            _rightFillImage.DOFillAmount(0f, _fillDuration).SetUpdate(true);
        }
    }

}