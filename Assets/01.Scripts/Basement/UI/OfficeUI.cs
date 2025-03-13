using Basement.Mission;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class OfficeUI : MonoBehaviour, IWindowPanel
    {
        public Office office;
        public Button moveLeftButton;
        public Button moveRightButton;
        public CharacterSelectPanel characterSelectPanel;
        public SkillTreePanel skillTreePanel;
        public MissionSelectPanel missionSelectPanel;


        private Dictionary<OfficeUIState, IWindowPanel> _officeUIDic;
        private OfficeUIState _currentUiState;

        private void Awake()
        {
            _officeUIDic = new Dictionary<OfficeUIState, IWindowPanel>();
            _officeUIDic.Add(OfficeUIState.MissionSelect, missionSelectPanel);
            _officeUIDic.Add(OfficeUIState.CharacterSelect, characterSelectPanel);
            characterSelectPanel.Init(this);

            moveLeftButton.onClick.AddListener(() => ChangeState(OfficeUIState.MissionSelect));
            moveRightButton.onClick.AddListener(() => ChangeState(OfficeUIState.CharacterSelect));
        }

        private void OnDisable()
        {
            moveLeftButton.onClick.RemoveAllListeners();
            moveRightButton.onClick.RemoveAllListeners();
        }

        private void ChangeState(OfficeUIState uiState)
        {
            if (_currentUiState != uiState) _officeUIDic[_currentUiState].Close();
            _currentUiState = uiState;
            _officeUIDic[_currentUiState].Open();

            moveRightButton.gameObject.SetActive(uiState == OfficeUIState.MissionSelect);
            moveLeftButton.gameObject.SetActive(uiState == OfficeUIState.CharacterSelect);
        }

        public void Open()
        {
            moveLeftButton.gameObject.SetActive(true);
            moveRightButton.gameObject.SetActive(true);
            ChangeState(OfficeUIState.CharacterSelect);
        }

        public void Close()
        {
            moveLeftButton.gameObject.SetActive(false);
            moveRightButton.gameObject.SetActive(false);
            _officeUIDic[_currentUiState].Close();
            UIManager.Instance.roomUI.Open();
            UIManager.Instance.basementUI.Open();
        }

        private enum OfficeUIState
        {
            CharacterSelect,
            MissionSelect,
        }
    }
}
