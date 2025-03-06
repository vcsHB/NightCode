using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Common
{

    public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _highlightScale = new Vector3(1.2f, 1.2f, 1f);
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
            _currentTween = transform.DOScale(_highlightScale, _duration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if (_currentTween.IsPlaying())
            //     _currentTween.Complete();
            _currentTween = transform.DOScale(_defaultScale, _duration);

        }
    }

}