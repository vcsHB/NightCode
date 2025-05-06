using DG.Tweening;
using System.Net.NetworkInformation;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Office
{
    public class OfficeUI : OfficeUIParent
    {
        public RectTransform background;

        public CharacterSelectPanel characterSelectPanel;
        //public MissionSelectPanel missionSelectPanel;
        public SkillTreePanel skillTreePanel;

        private OfficeUIParent _initialUI;
        [SerializeField] private RectTransform _returnBtnRect;
        [SerializeField] private GameObject _leftBtn, _rightBtn;
        [SerializeField] private float _duration;

        private bool _isCharacterPanel;
        private Sequence _openCloseSeq;
        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        private void Awake()
        {
            ChangeInitUI(true);
            background.anchoredPosition = new Vector2(0, screenPosition.y);
        }

        public void ChangeInitUI(bool isCharacterPanel)
        {
            _isCharacterPanel = isCharacterPanel;

            if (_isCharacterPanel) _initialUI = characterSelectPanel;
            //else _initialUI = missionSelectPanel;
        }

        public override void OpenAnimation()
        {
            if (_openCloseSeq != null && _openCloseSeq.active)
                _openCloseSeq.Kill();

            _openCloseSeq = DOTween.Sequence();
            _openCloseSeq.Append(background.DOAnchorPosY(0, _duration))
                .Join(_returnBtnRect.DOAnchorPosX(-900, _duration))
                .OnComplete(() =>
                {
                    _initialUI.Open();
                    OnCompleteOpen();
                });

            _leftBtn.SetActive(_isCharacterPanel);
            _rightBtn.SetActive(!_isCharacterPanel);
        }

        public override void CloseAnimation()
        {
            if (characterSelectPanel.isOpened)
            {
                characterSelectPanel.Close();
                if (characterSelectPanel.isOpened) return;
            }

            //if (missionSelectPanel.isOpened)
            //{
            //    missionSelectPanel.Close();
            //    if (missionSelectPanel.isOpened) return;
            //}


            if (_openCloseSeq != null && _openCloseSeq.active)
                _openCloseSeq.Kill();

            _openCloseSeq = DOTween.Sequence();
            _openCloseSeq.Append(background.DOAnchorPosY(screenPosition.y, _duration))
                .Join(_returnBtnRect.DOAnchorPosX(-screenPosition.x, _duration))
                .OnComplete(OnCompleteClose);

            _leftBtn.SetActive(false);
            _rightBtn.SetActive(false);
        }

        public void ChangeUI()
        {
            if (_isCharacterPanel)
            {
                _isCharacterPanel = false;

                characterSelectPanel.CloseAllUI();
                //characterSelectPanel.onCloseUI += missionSelectPanel.Open;
            }
            else
            {
                _isCharacterPanel = true;

                //missionSelectPanel.CloseAllUI();
                //missionSelectPanel.onCloseUI += characterSelectPanel.Open;
            }

            _leftBtn.SetActive(_isCharacterPanel);
            _rightBtn.SetActive(!_isCharacterPanel);
        }
    }
}
