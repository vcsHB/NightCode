using DG.Tweening;
using UnityEngine;

namespace UI
{

    public class UIMovePanel : UIPanel
    {
        [SerializeField] protected bool _isHorizontal;
        [SerializeField] protected float _defaultPos;
        [SerializeField] protected float _activePos;
        private RectTransform _rectTrm;

        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;
        }

        [ContextMenu("Debug Open")]

        public override void Open()
        {
            if (_isActive) return;
            base.Open();
            MovePanel(_activePos);

        }
        public override void Close()
        {
            if (!_isActive) return;
            base.Close();
            MovePanel(_defaultPos);
        }

        protected void MovePanel(float value)
        {
            if (_isHorizontal)
                _rectTrm.DOAnchorPosX(value, _activeDuration).SetUpdate(_useUnscaledTime);
            else
                _rectTrm.DOAnchorPosY(value, _activeDuration).SetUpdate(_useUnscaledTime);
        }
    }
}