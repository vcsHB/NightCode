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

        [SerializeField] private CharacterPanel[] _characterPanels;
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private SkillTreePanel _skillTreePanel;

        private Vector2[] _originPositions;
        private float _duration = 0.3f;

        private Tween _openCloseTween;
        private Sequence _selectPanelSeq;

        private RectTransform _rectTrm => transform as RectTransform;
        private Vector2 screenPos = new Vector2(Screen.width, Screen.height);


        private void Awake()
        {
            _originPositions = new Vector2[3];
            _skillTreePanel.InitSkillTree(_characterPanels[1].CharacterType);
            _rectTrm.anchoredPosition = new Vector2(screenPos.x, 0);

            for (int i = 0; i < 3; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_panelRect);

                _characterPanels[i].SetIndex(i);
                _originPositions[i] = _characterPanels[i].RectTrm.anchoredPosition;
            }
        }


        #region PanelSelectRegion

        public void MoveUp()
        {
            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Kill();

            _selectPanelSeq = DOTween.Sequence();

            _characterPanels[0].RectTrm.SetAsFirstSibling();
            _selectPanelSeq.Join(_characterPanels[0].RectTrm.DOAnchorPosY(_originPositions[2].y, _duration))
                .Join(_characterPanels[1].RectTrm.DOAnchorPosY(_originPositions[0].y, _duration))
                .Join(_characterPanels[2].RectTrm.DOAnchorPosY(_originPositions[1].y, _duration));

            _characterPanels[0].DisablePanel();
            _characterPanels[1].DisablePanel();
            _characterPanels[2].EnablePanel();


            _characterPanels[0].SetIndex(2);
            _characterPanels[1].SetIndex(0);
            _characterPanels[2].SetIndex(1);

            CharacterPanel temp = _characterPanels[0];
            _characterPanels[0] = _characterPanels[1];
            _characterPanels[1] = _characterPanels[2];
            _characterPanels[2] = temp;

            _skillTreePanel.InitSkillTree(_characterPanels[1].CharacterType);
        }


        public void MoveDown()
        {
            if (_selectPanelSeq != null && _selectPanelSeq.active)
                _selectPanelSeq.Kill();

            _selectPanelSeq = DOTween.Sequence();

            _characterPanels[2].RectTrm.SetAsFirstSibling();
            _selectPanelSeq.Join(_characterPanels[0].RectTrm.DOAnchorPosY(_originPositions[1].y, _duration))
                .Join(_characterPanels[1].RectTrm.DOAnchorPosY(_originPositions[2].y, _duration))
                .Join(_characterPanels[2].RectTrm.DOAnchorPosY(_originPositions[0].y, _duration));

            _characterPanels[0].EnablePanel();
            _characterPanels[1].DisablePanel();
            _characterPanels[2].DisablePanel();


            _characterPanels[0].SetIndex(1);
            _characterPanels[1].SetIndex(2);
            _characterPanels[2].SetIndex(0);

            CharacterPanel temp = _characterPanels[2];
            _characterPanels[2] = _characterPanels[1];
            _characterPanels[1] = _characterPanels[0];
            _characterPanels[0] = temp;

            _skillTreePanel.InitSkillTree(_characterPanels[1].CharacterType);
        }



        public void SelectCharacter(int index)
        {
            switch(index)
            {
                case 0:
                    MoveDown();
                    break;
                case 2:
                    MoveUp();
                    break;
            }
        }

        #endregion


        #region OpenCloseRegion

        public override void OpenAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = _rectTrm.DOAnchorPosX(0, _duration)
                    .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = _rectTrm.DOAnchorPosX(screenPos.x, _duration)
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
