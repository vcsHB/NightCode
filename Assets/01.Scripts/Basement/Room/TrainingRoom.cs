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
        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private Furniture _interactObject;
        private Transform _originFollow;
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

        public void OnInteractObject()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(1.5f, 0.4f);
        }

        public override void SetFactor(string factor)
        {
            //Factor: TrainingLevel
            _training = _trainingSetSO.GetTrainingSO(factor);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            OnInteractObject();
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(4f, 0.4f);
        }
    }
}
