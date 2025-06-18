using CameraControllers;
using Combat.PlayerTagSystem;
using UnityEngine;
namespace PerformanceSystem.CutScene
{

    public class CutSceneCameraController : MonoBehaviour
    {
        private CameraManager _cameraManager;
        private PlayerManager _playerManager;

        private void Start()
        {
            _cameraManager = CameraManager.Instance;
            _playerManager = PlayerManager.Instance;

        }
        public void EnableCameraFollow()
        {
            _cameraManager.SetConfinerFunction(true);
            _cameraManager.SetFollowFunction(true);
        }

        public void DisableCameraFollow()
        {
            _cameraManager.SetFollowFunction(false);

        }

        public void SetFollowPlayer()
        {
            _cameraManager.SetFollow(_playerManager.CurrentPlayerTrm);
        }
    }
}