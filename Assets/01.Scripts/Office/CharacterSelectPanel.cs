using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Basement.Training;
using Basement;
using UI;
using UnityEngine.UI;

namespace Office
{
    public class CharacterSelectPanel : OfficeUIParent
    {
        public event Action onCompleteReturnAnimation;

        [SerializeField] private CharacterPanel[] _characterPanels;
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private SkillTreePanel _skillTreePanel;

        private int _selectedIndex = -1;
        private Vector2[] _originPositions;
        private readonly Vector2 _selectedPosition = new Vector2(-1500, -390);
        private bool _isReturning = false;

        private Tween _openCloseTween;
        private Sequence _selectPanelSeq;

        private RectTransform _rectTrm => transform as RectTransform;


        private void Awake()
        {
            _originPositions = new Vector2[3];
            for (int i = 0; i < 3; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRect);

                _characterPanels[i].Init(i);
                _originPositions[i] = _characterPanels[i].RectTrm.anchoredPosition;
            }
        }


        #region PanelSelectRegion

        public void SelectCharacter(int index)
        {
            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Complete();

            _selectPanelSeq = DOTween.Sequence();


            if (_selectedIndex != -1)
                _selectPanelSeq.Join(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f));

            _selectPanelSeq.Join(_characterPanels[index].RectTrm.DOAnchorPos(_selectedPosition, 0.3f))
                    .Join(_panelRect.DOAnchorPosX(470, 0.3f))
                    .OnComplete(_skillTreePanel.Open);


            _selectedIndex = index;
            _skillTreePanel.onCloseUI += ReturnToSelectPanel;
        }

        public void ReturnToSelectPanel()
        {
            _isReturning = true;

            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Complete();

            _selectPanelSeq = DOTween.Sequence();
            _selectPanelSeq.Append(_panelRect.DOAnchorPosX(0, 0.3f))
                .Join(_characterPanels[_selectedIndex].RectTrm.DOAnchorPos(_originPositions[_selectedIndex], 0.3f))
                .OnComplete(() =>
                {
                    _isReturning = false;
                    onCompleteReturnAnimation?.Invoke();
                    LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRect);
                });

            _selectedIndex = -1;
        }

        #endregion


        #region OpenCloseRegion

        public override void OpenAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            for (int i = 0; i < 3; i++)
                _characterPanels[i].UpdateStat();

            _openCloseTween = _rectTrm.DOAnchorPosX(0, 0.3f)
                    .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_isReturning)
            {
                onCompleteReturnAnimation += CloseAnimation;
                return;
            }

            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = _rectTrm.DOAnchorPosX(470, 0.3f)
                    .OnComplete(OnCompleteClose);
        }

        public override void CloseAllUI()
        {
            if (linkedUI.isOpened)
            {
                linkedUI.Close();
                linkedUI.onCloseUI += Close;
            }
            else
            {
                Close();
            }
        }

        #endregion
    }
}
