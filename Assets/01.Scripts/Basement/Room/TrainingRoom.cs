using Basement.CameraController;
using Basement.Training;
using DG.Tweening;
using System.Text;
using UnityEngine;

namespace Basement
{
    public class TrainingRoom : BasementRoom
    {
        [SerializeField] private TrainingSetSO _trainingSetSO;
        [SerializeField] private Furniture _trainingFurniture;
        [SerializeField] private TrainingSO training;
        private TrainingSO _training;
        private CharacterEnum _selectedCharacter;
        private TrainingExplainUI _trainingExplain;

        private void OnEnable()
        {
            SetTraining(training);
            _trainingExplain = UIManager.Instance.trainingUI;
            _trainingFurniture.InteractAction += Training;
        }

        private void OnDisable()
        {
            _trainingFurniture.InteractAction -= Training;
        }

        private void Training()
        {
            _trainingExplain.gameObject.SetActive(true);
            _trainingExplain.SetTraining(training);
        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _trainingFurniture.InteractAction += Training;
            _trainingFurniture.Init(this);
        }

        public void SelectCharacter(CharacterEnum character)
            => _selectedCharacter = character;
    }
}
