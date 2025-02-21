using Basement.Training;
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
        private TrainingUI _trainingExplain;

        protected override void Awake()
        {
            base.Awake();
            furnitureList.Add(_trainingFurniture);
        }

        private void OnEnable()
        {
            _trainingFurniture.InteractAction += Training;
        }

        private void OnDisable()
        {
            _trainingFurniture.InteractAction -= Training;
        }

        private void Training()
        {
            _trainingExplain = UIManager.Instance.trainingUI;
            _trainingExplain.gameObject.SetActive(true);
            _trainingExplain.SetTraining(training);
        }

        public override void FocusRoom()
        {
            base.FocusRoom();

        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _trainingFurniture.InteractAction += Training;
            _trainingFurniture.Init(this);
        }

        public void SelectCharacter(CharacterEnum character)
            => _selectedCharacter = character;

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            SetTraining(training);
        }
    }
}
