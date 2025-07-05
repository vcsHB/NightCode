using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace UI.TitleScane
{

    public class LogoPanel : HorizontalPanel
    {
        public UnityEvent OnMoveOverEvent;
        private float _moveDuration = 0.5f;
        [SerializeField] private float _sidePosition = 120f;
        

        protected override void Awake()
        {
            base.Awake();
            _rectTrm.anchoredPosition = new Vector2(Camera.main.pixelWidth / 2f, _rectTrm.anchoredPosition.y);
        }

        public void MoveToSide()
        {
            _rectTrm.DOAnchorPosX(_sidePosition, _moveDuration).OnComplete(() => OnMoveOverEvent?.Invoke());
            _rectTrm.DOScale(Vector3.one, _moveDuration);
        }
    }
}