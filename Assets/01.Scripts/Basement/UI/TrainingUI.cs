using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.Training
{
    public class TrainingUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _fatigueText;
        [SerializeField] private Slider _fatigueSlider;
        [SerializeField] private Slider _fatiguePreviewSlider;
        [SerializeField] private TextMeshProUGUI _explainText;
        [SerializeField] private TMP_Dropdown _dropDown;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _checkButton;
        [SerializeField] private GameObject _alreadyTrainingText;
        [SerializeField] private GameObject _alreadyTrainigMark;

        [SerializeField] private List<Sprite> _iconList;
        private TrainingSO _training;

        private RectTransform _rectTrm => transform as RectTransform;
        private Tween _tween;

        private void Awake()
        {
            _dropDown.onValueChanged.AddListener(OnDropDownValueChange);
            _checkButton.onClick.AddListener(Training);
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(-10f, 0.3f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(500f, 0.3f);
        }

        private void Training()
        {
            CharacterEnum selectedCharacter = (CharacterEnum)_dropDown.value;
            TrainingManager.Instance.AddCharacterTraining(selectedCharacter, _training);
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
            OnDropDownValueChange(0);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTrm);
        }

        public void OnDropDownValueChange(int value)
        {
            CharacterEnum character = (CharacterEnum)value;

            bool isCharacterTraining = TrainingManager.Instance.TryGetTrainingInfo(character, out TrainingInfo info);
            _alreadyTrainingText.SetActive(isCharacterTraining);
            _alreadyTrainigMark.SetActive(isCharacterTraining);


            int fatigue = TrainingManager.Instance.GetFatigue(character);
            _fatigueText.SetText($"{fatigue}<color=red>+{_training.requireFatigue}");
            _fatigueSlider.value = fatigue / 100f;
            _fatiguePreviewSlider.value = (fatigue + _training.requireFatigue) / 100f;
            _iconImage.sprite = _iconList[value];
        }
    }
}
