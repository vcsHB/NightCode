using Basement.Training;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatIndicatorButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CharacterStatPointIndicator _statPointIndicator;
    [SerializeField] private CharacterEnum _characterType;

    private Tween _tween;
    private float _easingTime = 0.1f;

    private RectTransform RectTrm => transform as RectTransform;

    public void OnPointerClick(PointerEventData eventData)
    {
        _statPointIndicator.SetCharacter(_characterType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = RectTrm.DOScale(1.1f, _easingTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = RectTrm.DOScale(1f, _easingTime);
    }
}
