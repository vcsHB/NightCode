using DG.Tweening;
using UnityEngine;
namespace UI.SpeedRun
{

    public class SpeedRunTimePanel : UIPanel
    {
        [SerializeField] private float _moveDuration;
        [SerializeField] private Vector3 _disableScale;
        [SerializeField] private Vector2 _disablePosition;
        private RectTransform _rectTrm;

        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;
        }
        [ContextMenu("DebugOpen")]
        public override void Open()
        {
            base.Open();
            _rectTrm.DOAnchorMin(Vector2.zero, _moveDuration);
            _rectTrm.DOAnchorMax(Vector2.zero, _moveDuration);
            _rectTrm.DOScale(1f, _moveDuration);
            _rectTrm.DOAnchorPos(_disablePosition, _moveDuration);
        }

        public void ResetCenterPosition()
        {
            _rectTrm.DOAnchorMin(new Vector2(0.5f, 0.5f), _moveDuration);
            _rectTrm.DOAnchorMax(new Vector2(0.5f, 0.5f), _moveDuration);
            _rectTrm.DOScale(_disableScale, _moveDuration);
            _rectTrm.DOAnchorPos(Vector2.zero, _moveDuration);

        }


    }
}