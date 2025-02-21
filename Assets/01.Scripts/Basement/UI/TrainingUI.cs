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
        [SerializeField] private TextMeshProUGUI _explainText;
        [SerializeField] private TMP_Dropdown _dropDown;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _trainingButton;
        [SerializeField] private TrainingResultUI _trainingResultUI;

        [SerializeField] private List<Sprite> _iconList;
        private TrainingSO _training;

        private void Awake()
        {
            _dropDown.onValueChanged.AddListener(OnDropDownValueChange);
            _trainingButton.onClick.AddListener(Training);
        }

        private void Training()
        {
            CharacterEnum selectedCharacter = (CharacterEnum)_dropDown.value;
            TrainingResult result = _training.GetResult(selectedCharacter);

            int increaseValue = _training.increaseValue[result];
            TrainingManager.Instance.AddSkillPoint(selectedCharacter, _training.statType, increaseValue);

            int fatigue = UnityEngine.Random.Range(_training.minFatigue, _training.maxFatigue);
            TrainingManager.Instance.AddFatigue(selectedCharacter, fatigue);

            _trainingResultUI.gameObject.SetActive(true);
            _trainingResultUI.SetResult(result, _training.textColor[result], _training.statType, increaseValue, _iconImage.sprite);
            gameObject.SetActive(false);
        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _nameText.SetText(training.trainingVisibleName);
            _explainText.SetText(training.trainingExplain);
            OnDropDownValueChange(0);
        }

        public void OnDropDownValueChange(int value)
        {
            CharacterEnum character = (CharacterEnum)value;

            int fatigue = TrainingManager.Instance.GetFatigue(character);
            float fatigueCorrection = fatigueCorrection = (100f - fatigue) / 100f;
            float successChance = _training.successChance * fatigueCorrection;

            _fatigueText.SetText($"{character}\n피로도: {fatigue}\n성공확률: {successChance}%");
            _iconImage.sprite = _iconList[value];
        }
    }
}
