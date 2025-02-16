using Basement.CameraController;
using Basement.Player;
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

        private void OnEnable()
        {
            SetTraining(training);
        }

        private void OnDisable()
        {
            _trainingFurniture.InteractAction -= Training;
        }

        private void Training()
        {
            TrainingResult result = _training.GetResult(_selectedCharacter);

            int increaseValue = _training.increaseValue[result];
            TrainingManager.Instance.AddSkillPoint(_selectedCharacter, _training.statType, increaseValue);

            int fatigue = Random.Range(_training.minFatigue, _training.maxFatigue);
            TrainingManager.Instance.AddFatigue(_selectedCharacter, fatigue);
        }

        public void SetTraining(TrainingSO training)
        {
            _training = training;
            _trainingFurniture.InteractAction += Training;
            _trainingFurniture.Init(this);
        }

        public void SelectCharacter(CharacterEnum character)
            => _selectedCharacter = character;

        #region Factor

        public override void SetFactor(string factor)
        {
            string[] str = factor.Split(' ');

            int trainingId = int.Parse(str[0]);
            Vector2 position = new Vector2(float.Parse(str[1]), float.Parse(str[2]));
            _trainingFurniture.transform.localPosition = position;

            _training = _trainingSetSO.GetTrainingSO(trainingId);
            base.SetFactor(str[3]);
        }

        public override string GetFactor()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_training.trainingId);
            sb.Append(" ");
            sb.Append(_trainingFurniture.transform.localPosition.x);
            sb.Append(" ");
            sb.Append(_trainingFurniture.transform.localPosition.y);
            sb.Append(" ");
            sb.Append(base.GetFactor());

            return sb.ToString();
        }

        #endregion
    }
}
