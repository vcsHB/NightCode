using Basement.Mission;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class OfficeUI : BasementUI
    {
        [Space]
        public Office office;

        //init panel
        public CharacterSelectPanel characterSelectPanel;
        public SkillTreePanel skillTreePanel;

        //next panel
        public MissionSelectPanel missionSelectPanel;

        public RectTransform bgRect;
        public Button returnBtn;
        public BasementUI returnBtnUI => returnBtn.GetComponent<BasementUI>();

        private Tween _bgTween;

        private void Awake()
        {
            skillTreePanel.Init(this);
            characterSelectPanel.Init(this);
            characterSelectPanel.SetUILink(skillTreePanel);
        }

        protected override void OpenAnimation()
        {
            _bgTween = bgRect.DOAnchorPosY(0, 0.2f)
                .OnComplete(() =>
                {
                    characterSelectPanel.SetOppositeUI(missionSelectPanel);

                    returnBtnUI.Open();
                    characterSelectPanel.Open();
                    OnCompleteOpenAction();
                });
        }

        protected override void CloseAnimation()
        {
            if (characterSelectPanel.isLinkedUIOpend)
            {
                characterSelectPanel.Close();
                isOpend = true;
                return;
            }


            _bgTween = bgRect.DOAnchorPosY(1080, 0.2f);

            characterSelectPanel.RemoveOppositeUI(true);
            characterSelectPanel.CloseAllUI();
            returnBtnUI.Close();

            UIManager.Instance.roomUI.Open();
            UIManager.Instance.basementUI.Open();
            OnCompleteCloseAction();
        }
    }
}
