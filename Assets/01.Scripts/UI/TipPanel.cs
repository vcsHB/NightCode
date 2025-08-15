using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _explainPanel;
    [SerializeField] private Button _leftButton, _rightButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private List<float> _positions;
    private Tween _moveTween;

    private int _currentIndex = 0;
    private int MaxIndex => _positions.Count;

    private void Awake()
    {
        _closeButton.onClick.AddListener(Close);
        _leftButton.onClick.AddListener(GoLeft);
        _rightButton.onClick.AddListener(GoRight);

        Refresh();
        CheckButton();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0;
    }
    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    public void Refresh()
    {
        _currentIndex = 0;
        _explainPanel.anchoredPosition = new Vector2(_positions[0], _explainPanel.anchoredPosition.y);
    }

    public void GoLeft()
    {
        if (_currentIndex == 0) return;
        if (_moveTween != null && _moveTween.active) return;

        _moveTween = _explainPanel.DOAnchorPosX(_positions[--_currentIndex], 0.3f)
            .SetEase(Ease.OutCubic).SetUpdate(true);
        CheckButton();
    }

    public void GoRight()
    {
        if (_currentIndex == (MaxIndex - 1)) return;
        if (_moveTween != null && _moveTween.active) return;

        _moveTween = _explainPanel.DOAnchorPosX(_positions[++_currentIndex], 0.3f)
            .SetEase(Ease.OutCubic).SetUpdate(true);
        CheckButton();
    }

    private void CheckButton()
    {
        _leftButton.gameObject.SetActive(_currentIndex != 0);
        _rightButton.gameObject.SetActive(_currentIndex != (MaxIndex - 1));

        if (_currentIndex == (MaxIndex - 1))
            _closeButton.gameObject.SetActive(true);
    }
}
