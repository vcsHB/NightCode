using DG.Tweening;
using UnityEngine;
namespace UI
{

    public class HorizontalPanel : UIPanel
    {
        [SerializeField] private float _height;
        protected RectTransform _rectTrm;
        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;
        }

        public override void Open()
        {
            base.Open();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, _height), _activeDuration).SetUpdate(true);
        }

        public override void Close()
        {
            base.Close();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration).SetUpdate(true);
            
        }
    }
}