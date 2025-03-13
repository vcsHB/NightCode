using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Basement.Training;
using Basement;
using UI;
using UnityEngine.UI;

public class CharacterSelectPanel : MonoBehaviour, IWindowPanel
{
    public event Action onCompleteAnimation;

    [SerializeField] private CharacterPanel[] _characterPanels;
    [SerializeField] private RectTransform _panelRect;

    private Tween _tween;
    private Sequence _seq;
    private OfficeUI _officeUI;
    private int _selectedIndex = -1;
    private Vector2[] _originPositions;
    private readonly Vector2 _selectedPosition = new Vector2(-1500, -390);

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
            if (_seq != null && _seq.active)
                _seq.Complete();

            _seq = DOTween.Sequence();
            _seq.Append(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f))
                .Join(_characterPanels[index].RectTrm.DOAnchorPos(_selectedPosition, 0.3f))
                .Join(_panelRect.DOAnchorPosX(470, 0.3f))
                .OnComplete(_officeUI.skillTreePanel.Open);
        }
        else
        {
            if (_seq != null && _seq.active)
                _seq.Complete();

            _seq = DOTween.Sequence();
            _seq.Append(_characterPanels[index].RectTrm.DOAnchorPos(_selectedPosition, 0.3f))
                .Join(_panelRect.DOAnchorPosX(470, 0.3f))
                .OnComplete(_officeUI.skillTreePanel.Open);
        }

        UIManager.Instance.returnButton.ChangeReturnAction(ReturnToSelectPanel);
        _selectedIndex = index;
    }

    public void ReturnToSelectPanel()
    {
        if (_seq != null && _seq.active)
            _seq.Complete();

        _seq = DOTween.Sequence();
        _seq.Append(_panelRect.DOAnchorPosX(0, 0.3f))
            .Join(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f))
            .OnStart(() =>
            {
                _officeUI.skillTreePanel.Close();
                _officeUI.office.ReturnButtonCloseAllUI();
               // LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRect);
            });

        _selectedIndex = -1;
    }

    public void Open()
    {
        if (_tween != null && _tween.active)
            _tween.Kill();

        for (int i = 0; i < 3; i++)
            _characterPanels[i].UpdateStat();

        _tween = _rectTrm.DOAnchorPosX(0, 0.3f);
    }

    public void Close()
    {
        if (_selectedIndex != -1)
        {
            ReturnToSelectPanel();
            _officeUI.skillTreePanel.Close();
            return;
        }

        if (_tween != null && _tween.active)
            _tween.Kill();

        _tween = _rectTrm.DOAnchorPosX(470, 0.3f);
    }
}
