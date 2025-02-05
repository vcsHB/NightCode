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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = _switchTrm.DOAnchorPosY(, 0.2f);
    }
}
