using Basement.Training;
using UnityEngine;

namespace Basement
{
    public class TrainingRoom : BasementRoom
    {
        [SerializeField] private TrainingSetSO _trainingSetSO;
        [SerializeField] private Furniture _trainingFurniture;
        [SerializeField] private TrainingSO training;
        private TrainingUI _trainingExplain;

        protected override void Awake()
        {
            base.Awake();
            furnitureList.Add(_trainingFurniture);
        }

        private void OnEnable()
        {
            _trainingFurniture.InteractAction += Training;
            _trainingExplain = UIManager.Instance.GetUIPanel(BasementRoomType.TrainingRoom) as TrainingUI;
        }

        private void OnDisable()
        {
            _trainingFurniture.InteractAction -= Training;
        }

        private void Training()
        {
            _trainingExplain.Open();
            _trainingExplain.SetTraining(training);
        }

        public void SetTraining(TrainingSO training)
        {
            _trainingFurniture.InteractAction += Training;
            _trainingFurniture.Init(this);
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            SetTraining(training);
        }

        protected override void CloseUI()
        {
            _trainingExplain.Close();
        }
    }
}
