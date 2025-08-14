using Chipset;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Transform _rewardIconParnet;
    [SerializeField] private Image _rewardIconPrefab;
    [SerializeField] private Button _closeButton;

    private CanvasGroup _canvsGroup => GetComponent<CanvasGroup>();
    private Tween _openCloseTween;

    private void Awake()
    {
        _closeButton.onClick.AddListener(Close);
    }

    public void SetReward(List<ChipsetSO> chipsetSO)
    {
        chipsetSO.ForEach(chipset => 
        {
            Image icon = Instantiate(_rewardIconPrefab, _rewardIconParnet);
            icon.sprite = chipset.icon;
        });
    }

    public void Open()
    {
        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _openCloseTween = _canvsGroup.DOFade(1, 0.3f)
            .OnComplete(() =>
            {
                _canvsGroup.interactable = true;
                _canvsGroup.blocksRaycasts = true;
            });
    }
    public void Close()
    {

        if (_openCloseTween != null && _openCloseTween.active)
            _openCloseTween.Kill();

        _openCloseTween = _canvsGroup.DOFade(0, 0.3f)
            .OnComplete(() =>
            {
                _canvsGroup.interactable = false;
                _canvsGroup.blocksRaycasts = false;
            });
    }
}
