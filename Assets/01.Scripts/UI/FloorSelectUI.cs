using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class FloorSelectUI : MonoBehaviour, IUIPanel
{
    [SerializeField] private RectTransform _selection;
    [SerializeField] private FloorSelectButton[] floorSelectBtn;
    private Tween _tween;

    public RectTransform RectTrm => transform as RectTransform;

    private void Awake()
    {

    }

    public void SetFloor(int floor)
    {
        if (_tween != null && _tween.active)
            _tween.Kill();


    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open(Vector2 position)
    {
        gameObject.SetActive(true);
        RectTrm.anchoredPosition = position;
    }
}
