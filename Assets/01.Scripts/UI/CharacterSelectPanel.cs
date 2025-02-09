using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Basement.Training;

public class CharacterSelectPanel : MonoBehaviour, IUIPanel
{
    public event Action onCompleteAnimation;

    [SerializeField] private CharacterPanel[] _characterPanels;
    [SerializeField] private TextMeshProUGUI _pageText;
    [SerializeField] private RectTransform _btnParent;

    private Vector2[] _positions = new Vector2[3];
    private CharacterEnum _currentCharacter;
    private Tween _tween;
    private int _currentIdx = 1;

    private Sequence _seq;

    public int CurrentIndex => _currentIdx;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            _characterPanels[i].Init(i + 1);
            _positions[i] = _characterPanels[i].RectTrm.anchoredPosition;
        }

        _characterPanels[0].UpdateStat();
    }

    public void MoveToNextCharacter()
    {
        if (_seq != null && _seq.active)
            return;

        _characterPanels[1].UpdateStat();
        Vector2 offset = new Vector2(-150, 0);

        _seq = DOTween.Sequence();
        _seq.Append(_characterPanels[0].RectTrm.DOAnchorPos(_positions[0] + offset, 0.2f))
            .AppendInterval(0.05f)
            .AppendCallback(() =>
            {
                _characterPanels[0].RectTrm.SetAsFirstSibling();
                _currentIdx = _currentIdx == 3 ? 1 : _currentIdx + 1;
                _pageText.SetText($"( {_currentIdx} / 3 )");
            })
            .AppendInterval(0.05f)
            .Append(_characterPanels[0].RectTrm.DOAnchorPos(_positions[2], 0.3f))
            .Join(_characterPanels[1].RectTrm.DOAnchorPos(_positions[0], 0.1f))
            .Join(_characterPanels[2].RectTrm.DOAnchorPos(_positions[1], 0.1f))
            .OnComplete(() =>
            {
                CharacterPanel panelTemp = _characterPanels[0];
                _characterPanels[0] = _characterPanels[1];
                _characterPanels[1] = _characterPanels[2];
                _characterPanels[2] = panelTemp;
                onCompleteAnimation?.Invoke();
            });
    }

    public void MoveToPrevCharacter()
    {
        if (_seq != null && _seq.active)
            return;

        _characterPanels[2].UpdateStat();
        Vector2 offset = new Vector2(-150, 0);

        _seq = DOTween.Sequence();
        _seq.Append(_characterPanels[2].RectTrm.DOAnchorPos(_positions[0] + offset, 0.2f))
            .AppendInterval(0.05f)
            .AppendCallback(() =>
            {
                _characterPanels[2].RectTrm.SetAsLastSibling();
                _currentIdx = _currentIdx == 1 ? 3 : _currentIdx - 1;
                _pageText.SetText($"( {_currentIdx} / 3 )");
            })
            .AppendInterval(0.05f)
            .Append(_characterPanels[2].RectTrm.DOAnchorPos(_positions[0], 0.3f))
            .Join(_characterPanels[0].RectTrm.DOAnchorPos(_positions[1], 0.1f))
            .Join(_characterPanels[1].RectTrm.DOAnchorPos(_positions[2], 0.1f))
            .OnComplete(() =>
            {
                CharacterPanel panelTemp = _characterPanels[2];
                _characterPanels[2] = _characterPanels[1];
                _characterPanels[1] = _characterPanels[0];
                _characterPanels[0] = panelTemp;
                onCompleteAnimation?.Invoke();
            });
    }

    public void SelectCharacter()
    {
        if (_seq != null && _seq.active)
        {
            _seq.Complete();
        }

        _characterPanels[1].UpdateStat();

        float left = -650;
        float right = 1215;
        _seq = DOTween.Sequence();
        _seq.AppendInterval(0.2f)
            .Append(_characterPanels[0].RectTrm.DOAnchorPosX(left, 0.3f))
            .Join(_characterPanels[0].RectTrm.DORotate(Vector3.zero, 0.3f))
            .Join(_btnParent.DOAnchorPosY(-600, 0.2f))
            .Insert(0.1f, _characterPanels[1].RectTrm.DOAnchorPosX(right, 0.3f))
            .Insert(0.2f, _characterPanels[2].RectTrm.DOAnchorPosX(right, 0.3f))
            .OnComplete(() =>
            {
                onCompleteAnimation?.Invoke();
            });
    }

    public void ReturnToSelectPanel()
    {
        if (_seq != null && _seq.active)
            return;

        _characterPanels[0].UpdateStat();
        _characterPanels[0].isSelected = false;
        UIManager.Instance.GetUIPanel(UIType.SkillTreePanel).Close();

        _seq = DOTween.Sequence();
        _seq.AppendInterval(0.5f)
            .Append(_characterPanels[1].RectTrm.DOAnchorPos(_positions[1], 0.3f))
            .Join(_characterPanels[0].RectTrm.DORotate(new Vector3(0, 0, 5), 0.3f))
            .Join(_btnParent.DOAnchorPosY(-400, 0.2f))
            .Insert(0.1f, _characterPanels[2].RectTrm.DOAnchorPos(_positions[2], 0.3f))
            .Insert(0.3f, _characterPanels[0].RectTrm.DOAnchorPos(_positions[0], 0.3f))
            .OnComplete(() =>
            {
                onCompleteAnimation?.Invoke();
            });
    }


    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open(Vector2 position)
    {
        gameObject.SetActive(true);
    }
}
