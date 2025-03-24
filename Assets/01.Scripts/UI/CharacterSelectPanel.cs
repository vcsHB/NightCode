using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Basement.Training;
using Basement;
using UI;
using UnityEngine.UI;

public class CharacterSelectPanel : BasementUI
{
    public event Action onCompleteReturnAnimation;

    [SerializeField] private CharacterPanel[] _characterPanels;
    [SerializeField] private RectTransform _panelRect;

    private OfficeUI _officeUI;
    private int _selectedIndex = -1;
    private Vector2[] _originPositions;
    private readonly Vector2 _selectedPosition = new Vector2(-1500, -390);
    private bool _isReturning = false;

    private Tween _openCloseTween;
    private Sequence _selectPanelSeq;

    private RectTransform _rectTrm => transform as RectTransform;

    public void Init(OfficeUI officeUi)
    {
        _officeUI = officeUi;

        _originPositions = new Vector2[3];
        for (int i = 0; i < 3; i++)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRect);

            _characterPanels[i].Init(i);
            _originPositions[i] = _characterPanels[i].RectTrm.anchoredPosition;
        }
    }

    public void SelectCharacter(int index)
    {
        if (_selectedIndex != -1)
        {
            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Complete();

            _selectPanelSeq = DOTween.Sequence();
            _selectPanelSeq.Append(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f))
                .Join(_characterPanels[index].RectTrm.DOAnchorPos(_selectedPosition, 0.3f))
                .Join(_panelRect.DOAnchorPosX(470, 0.3f))
                .OnComplete(_officeUI.skillTreePanel.Open);
        }
        else
        {
            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Complete();

            _selectPanelSeq = DOTween.Sequence();
            _selectPanelSeq.Append(_characterPanels[index].RectTrm.DOAnchorPos(_selectedPosition, 0.3f))
                .Join(_panelRect.DOAnchorPosX(470, 0.3f))
                .OnComplete(_officeUI.skillTreePanel.Open);
        }

        //UIManager.Instance.returnButton.ChangeReturnAction(ReturnToSelectPanel);
        _selectedIndex = index;
    }

    public void ReturnToSelectPanel()
    {
        _isReturning = true;
        //_officeUI.skillTreePanel.Close();
        //_officeUI.office.ReturnButtonCloseAllUI();

        if (_selectPanelSeq != null && _selectPanelSeq.active)
            _selectPanelSeq.Complete();

        _selectPanelSeq = DOTween.Sequence();
        _selectPanelSeq.Append(_panelRect.DOAnchorPosX(0, 0.3f))
            .Join(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f))
            .OnComplete(() =>
            {
                _isReturning = false;
                onCompleteReturnAnimation?.Invoke();
            });

        _selectedIndex = -1;
    }

    protected override void OpenAnimation()
    {
        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        for (int i = 0; i < 3; i++)
            _characterPanels[i].UpdateStat();

        _openCloseTween = _rectTrm.DOAnchorPosX(0, 0.3f)
                .OnComplete(OnCompleteOpenAction);
    }

    protected override void CloseAnimation()
    {
        //if (_selectedIndex != -1)
        //{
        //    ReturnToSelectPanel();
        //    return;
        //}

        if (_isReturning)
        {
            onCompleteReturnAnimation += CloseAnimation;
            return;
        }

        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _openCloseTween = _rectTrm.DOAnchorPosX(470, 0.3f)
                .OnComplete(OnCompleteCloseAction);
    }
}
