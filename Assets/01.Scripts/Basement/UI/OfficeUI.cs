using Basement.Mission;
using System;
using System.Collections.Generic;
using UI;
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

        public Button returnBtn;
        public BasementUI returnBtnUI => returnBtn.GetComponent<BasementUI>();

        private void Awake()
        {
            skillTreePanel.Init(this);
            characterSelectPanel.Init(this);
            characterSelectPanel.SetUILink(skillTreePanel);
        }

        protected override void OpenAnimation()
        {
            characterSelectPanel.SetOppositeUI(missionSelectPanel);

            returnBtnUI.Open();
            characterSelectPanel.Open();
            onCompleteClose?.Invoke();
        }

        protected override void CloseAnimation()
        {
            characterSelectPanel.RemoveOppositeUI(true);
            characterSelectPanel.CloseAllUI();
            returnBtnUI.Close();

            UIManager.Instance.roomUI.Open();
            UIManager.Instance.basementUI.Open();
            onCompleteClose?.Invoke();
        }
    }
}
