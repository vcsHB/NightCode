using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Common
{

    public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnHoverEnterEvent;
        public UnityEvent OnHoverExitEvent;
        [SerializeField] private Vector3 _highlightScale = new Vector3(1.2f, 1.2f, 1f);
        [SerializeField] private bool _useUnscaledTime;
        private Vector3 _defaultScale;
        [SerializeField] private float _duration = 0.1f;
        private Tween _currentTween;


        private void Awake()
        {
            _defaultScale = transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            // if (_currentTween.IsPlaying())
            //     _currentTween.Complete();
            OnHoverEnterEvent?.Invoke();
            _currentTween = transform.DOScale(_highlightScale, _duration).SetUpdate(_useUnscaledTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if (_currentTween.IsPlaying())
            //     _currentTween.Complete();
            OnHoverExitEvent?.Invoke();
            _currentTween = transform.DOScale(_defaultScale, _duration).SetUpdate(_useUnscaledTime);

        }
    }

}