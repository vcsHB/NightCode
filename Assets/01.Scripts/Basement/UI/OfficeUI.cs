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

        private RoomUI _roomUI;
        public BasementUI returnBtnUI => returnBtn.GetComponent<BasementUI>();

        private void Awake()
        {
            skillTreePanel.Init(this);
            characterSelectPanel.Init(this);
            characterSelectPanel.SetUILink(skillTreePanel);
            _roomUI = UIManager.Instance.roomUI;
        }

        protected override void OpenAnimation()
        {
            //SetOppositeUI(_roomUI);
            characterSelectPanel.SetOppositeUI(missionSelectPanel);

            returnBtnUI.Open();
            characterSelectPanel.Open();
            OnCompleteOpenAction();
        }

        protected override void CloseAnimation()
        {
            if(characterSelectPanel.isLinkedUIOpend)
            {
                characterSelectPanel.Close();
                isOpend = true;
                return;
            }
            characterSelectPanel.RemoveOppositeUI(true);
            characterSelectPanel.CloseAllUI();
            returnBtnUI.Close();

            UIManager.Instance.roomUI.Open();
            UIManager.Instance.basementUI.Open();
            OnCompleteCloseAction();
        }
    }
}
