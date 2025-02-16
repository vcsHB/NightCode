using Basement.CameraController;
using Basement.Player;
using Basement.Training;
using DG.Tweening;
using UnityEngine;

namespace Basement
{
    public class TrainingRoom : BasementRoom
    {
        [SerializeField] private TrainingSetSO _trainingSetSO;
        [SerializeField] private TrainingSO _training;
        [SerializeField] private Furniture _interactObject;
        private CharacterEnum _selectedCharacter;

        private void OnEnable()
        {
            _interactObject.InteractAction += Training;
            _interactObject.Init(FindAnyObjectByType<BasementPlayer>());
        }

        private void OnDisable()
        {
            _interactObject.InteractAction -= Training;
        }

        private void Training()
        {
            TrainingResult result = _training.GetResult(_selectedCharacter);

            int increaseValue = _training.increaseValue[result];
            TrainingManager.Instance.AddSkillPoint(_selectedCharacter, _training.statType, increaseValue);

            int fatigue = Random.Range(_training.minFatigue, _training.maxFatigue);
            TrainingManager.Instance.AddFatigue(_selectedCharacter, fatigue);
        }

        public void SelectCharactere(CharacterEnum character)
            => _selectedCharacter = character;

        
        public override void SetFactor(string factor)
        {
            //Factor: TrainingLevel
            _training = _trainingSetSO.GetTrainingSO(factor);
        }
    }
}
