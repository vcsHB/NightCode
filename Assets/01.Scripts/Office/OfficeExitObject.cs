using Core.StageController;
using UnityEngine;

namespace Base.Office
{
    public class OfficeExitObject : BaseInteractiveObject
    {
        public override void OnPlayerInteract()
        {
            OnInteract();
            //_player.AddInteract(OnInteract);
        }

        public override void OnPlayerInteractExit()
        {
           // OnInteract
            //_player.RemoveInteract(OnInteract);
        }

        private void OnInteract()
        {
            //TODO: Open UI and let going to next stage
            StageManager.Instance.LoadNextStage();
        }
    }
}
