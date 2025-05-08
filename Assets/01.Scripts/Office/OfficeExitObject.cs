using Core.StageController;
using Office;
using UnityEngine;

namespace Base.Office
{
    public class OfficeExitObject : BaseInteractiveObject
    {
        [SerializeField] private CharacterFormation _characterFormation;

        public override void OnPlayerInteract()
        {
            _player.AddInteract(OnInteract);
        }

        public override void OnPlayerInteractExit()
        {
            _player.RemoveInteract(OnInteract);
        }

        private void OnInteract()
        {
            _characterFormation.Init(StageManager.Instance.GetNextStage());
            _characterFormation.Open();

            //TODO: Open UI and let going to next stage
            //StageManager.Instance.LoadNextStage();
        }
    }
}
