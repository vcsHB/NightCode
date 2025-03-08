using Basement.Training;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class LodgingUI : BasementRoomCharacterPlaceUI
    {
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Slider _fatigueSlider;
        [SerializeField] private Slider _fatiguePrevieSlider;
        [SerializeField] private TextMeshProUGUI _fatigueText;
        [SerializeField] private TextMeshProUGUI _restTimeText;
        [SerializeField] private Button _checkButton;
        [SerializeField] private TextMeshProUGUI _checkButtonText;


        private Tween _tween;
        private int _restTime = 30;
        private Lodging _lodging;

        private RectTransform _rectTrm => transform as RectTransform;

        public void Init(Lodging lodging)
        {
            _lodging = lodging;

            if (lodging.IsCharacterPlaced)
            {
                OnSelectCharacter((int)lodging.characterEnum);
                _checkButton.onClick.AddListener(CancelCharacterRest);
                _checkButton.onClick.RemoveListener(SetCharacterRest);
                _checkButtonText.SetText("ÈÞ½Ä Ãë¼Ò");
            }
            else
            {
                OnSelectCharacter(0);
                _checkButton.onClick.AddListener(SetCharacterRest);
                _checkButton.onClick.RemoveListener(CancelCharacterRest);
                _checkButtonText.SetText("ÈÞ½Ä ½ÃÀÛ");
            }
        }

        protected override void OnSelectCharacter(int value)
        {
            base.OnSelectCharacter(value);

            _restTime = 30;
            SetFatigueText();
            SetTimerText();
        }

        public void AddRestTime()
        {
            _restTime += 30;
            _restTime = Mathf.Clamp(_restTime, 30, _lodging.GetMaxRestTime());
            SetTimerText();
            SetFatigueText();
        }

        public void MinusRestTime()
        {
            _restTime -= 30;
            _restTime = Mathf.Clamp(_restTime, 30, _lodging.GetMaxRestTime());
            SetTimerText();
            SetFatigueText();
        }

        public void SetTimerText()
        {
            string hour = string.Format("{0,2:D2}", _restTime / 60);
            string minite = string.Format("{0,2:D2}", _restTime % 60);
            _restTimeText.SetText($"ÈÞ½Ä ½Ã°£: {hour}h{minite}m");
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(0, 0.2f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(500, 0.2f);
        }

        public void SetCharacterRest()
        {
            _lodging.SetCharacter(_selectedCharacter);
            WorkManager.Instance.AddRoomAction(_selectedCharacter, _restTime, "ÈÞ½Ä Áß...", OnCompleteRest);
            Close();
        }

        public void CancelCharacterRest()
        {
            _lodging.CancelplaceCharacter();
            WorkManager.Instance.RemoveRoomAction(_selectedCharacter);
            Close();
        }

        private void OnCompleteRest(CharacterEnum character)
        {
            int fatigueDecrease = _lodging.GetFatigueDecreaseValue(_restTime);
            CharacterManager.Instance.AddFatigue(character, -fatigueDecrease);
        }

        private void SetFatigueText()
        {
            int fatigue = CharacterManager.Instance.GetFatigue(_selectedCharacter);
            int fatigueDecrease = _lodging.GetFatigueDecreaseValue(_restTime);
            _fatigueSlider.value = (float)fatigue / 100f;
            _fatiguePrevieSlider.value = (float)(fatigue - fatigueDecrease) / 100f;
            _fatigueText.SetText($"{fatigue}<color=blue>-{fatigueDecrease}</color>");
        }
    }
}
