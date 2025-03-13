using Basement.Training;
using UnityEngine;

namespace Basement
{
    public class TrainingRoom : BasementRoom
    {
        [SerializeField] private TrainingSetSO _trainingSetSO;
        [SerializeField] private Furniture _trainingFurniture;
        [SerializeField] private TrainingSO _training;
        private TrainingUI _trainingExplain;



        protected override void Awake()
        {
            base.Awake();
            furnitureList.Add(_trainingFurniture);
        }

        private void OnEnable()
        {
            _trainingFurniture.InteractAction += OpenTrainingUI;
            _trainingExplain = UIManager.Instance.GetUIPanel(BasementRoomType.TrainingRoom) as TrainingUI;
        }

        private void OnDisable()
        {
            _trainingFurniture.InteractAction -= OpenTrainingUI;
        }

        private void OpenTrainingUI()
        {
            _trainingExplain.Open();
            _trainingExplain.SetTraining(_training);
            UIManager.Instance.basementUI.Close();
        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _trainingFurniture.InteractAction += OpenTrainingUI;
            _trainingFurniture.Init(this);
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            SetTraining(_training);
        }

        public override void CloseUI()
        {
            _trainingExplain.Close();
            UIManager.Instance.basementUI.Open();
        }
    }
}
