using Core.StageController;
using Office;
using UnityEngine;

namespace Base.Office
{
    public class OfficeExitObject : BaseInteractiveObject
    {
        [SerializeField] private CharacterFormation _characterFormation;
        [SerializeField] private OfficeNextStageWarningPanel _officeNextStageWarningPanel;

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
            if(OfficeManager.Instance.office.CheckReadEssentialDialog() == false)
            {
                //필수 대화가 끝나지 않았습니다. 경고를 해주
                _officeNextStageWarningPanel.Open();
                Time.timeScale = 0;
                return;
            }

            _characterFormation.Init(StageManager.Instance.GetNextStage());
            _characterFormation.Open();

            //TODO: Open UI and let going to next stage
            //StageManager.Instance.LoadNextStage();
        }
    }
}
