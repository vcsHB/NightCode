using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toggle : MonoBehaviour, IPointerClickHandler
{
    public Action<bool> onValueChange;

    [SerializeField] private RectTransform _switchTrm;
    [SerializeField] private Vector2 _positions;
    private Tween _tween;

    public bool IsOn = false;

    private void OnEnable()
    {
        _switchTrm.anchoredPosition = new Vector2(0, IsOn ? _positions.x : _positions.y);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tween != null && _tween.active)
            _tween.Kill();

        IsOn = !IsOn;
        _tween = _switchTrm.DOAnchorPosY(IsOn ? _positions.x : _positions.y, 0.2f)
            .OnComplete(() => onValueChange?.Invoke(IsOn));
    }
}
