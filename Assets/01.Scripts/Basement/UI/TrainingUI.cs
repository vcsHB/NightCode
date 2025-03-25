using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.Training
{
    public class TrainingUI : BasementRoomCharacterPlaceUI
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _fatigueText;
        [SerializeField] private Slider _fatigueSlider;
        [SerializeField] private Slider _fatiguePreviewSlider;
        [SerializeField] private TextMeshProUGUI _explainText;
        [SerializeField] private Button _checkButton;

        private TrainingSO _training;

        private RectTransform _rectTrm => transform as RectTransform;
        private Tween _tween;

        protected override void Awake()
        {
            base.Awake();
            _checkButton.onClick.AddListener(Training);
        }

        protected override void OpenAnimation()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(-10f, 0.3f)
                .OnComplete(() => onCompleteOpen?.Invoke());
        }
        protected override void CloseAnimation()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(500f, 0.3f)
                .OnComplete(() => onCompleteClose?.Invoke());
        }

        private void Training()
        {
            CharacterEnum selectedCharacter = (CharacterEnum)characterSelectDropDown.value;
            WorkManager.Instance.AddRoomAction(selectedCharacter, _training.requireTime, $"{_nameText.text}중...", OnCompleteTraining);
            Close();
            //TrainingResult result = _training.GetResult(selectedCharacter);

            //int increaseValue = _training.increaseValue[result];
            //TrainingManager.Instance.AddSkillPoint(selectedCharacter, _training.statType, increaseValue);

            //int fatigue = UnityEngine.Random.Range(_training.minFatigue, _training.maxFatigue);
            //TrainingManager.Instance.AddFatigue(selectedCharacter, fatigue);

            //_trainingResultUI.gameObject.SetActive(true);
            //_trainingResultUI.SetResult(result, _training.textColor[result], _training.statType, increaseValue, _iconImage.sprite);
        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _nameText.SetText(training.trainingVisibleName);
            _explainText.SetText(training.trainingExplain);
            OnSelectCharacter(0);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTrm);
        }

        protected override void OnSelectCharacter(int value)
        {
            base.OnSelectCharacter(value);
            
            int fatigue = CharacterManager.Instance.GetFatigue(_selectedCharacter);

            _fatigueText.SetText($"{fatigue}<color=red>+{_training.requireFatigue}");
            _fatigueSlider.value = fatigue / 100f;
            _fatiguePreviewSlider.value = (fatigue + _training.requireFatigue) / 100f;
        }

        private void OnCompleteTraining(CharacterEnum character)
        {
            TrainingResult result = _training.GetResult(character);
            int increaseValue = _training.increaseValue[result];
            int increaseFatigue = _training.requireFatigue;

            CharacterManager.Instance.AddFatigue(character, increaseFatigue);
            CharacterManager.Instance.AddSkillPoint(character, _training.statType, increaseValue);

            string trainingCompleteText = $"{_training.trainingVisibleName} Complete\n피로도 +{increaseFatigue}    {_training.statType.ToString()} pt +{increaseValue}";
            //UIManager.Instance.msgText.PopMSGText(character, trainingCompleteText);
        }
    }
}
