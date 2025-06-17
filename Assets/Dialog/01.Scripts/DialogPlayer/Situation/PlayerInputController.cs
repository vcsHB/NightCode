using Core.StageController;
using InputManage;
using UnityEngine;
namespace Dialog.SituationControl
{

    public class PlayerInputController : SituationElement
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerInputStatus newInputStatus;

        public override void StartSituation()
        {
            _playerInput.playerInputStatus = newInputStatus;
        }

        public override void EndSituation()
        {
            _playerInput.SetEnabledAllStatus();
        }

    }
}