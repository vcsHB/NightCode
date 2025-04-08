using CameraControllers;
using UnityEngine;
namespace Office.Armory
{

    public class WeaponCameraHolder : MonoBehaviour
    {
        [SerializeField] private float _newZoomLevel = 12f;
        [SerializeField] private float _zoomDuration = 1f;
        private CameraManager _cameraManager;
        private CameraZoomController _zoomController;
        private void Awake()
        {
            _cameraManager = CameraManager.Instance;
            _zoomController = _cameraManager.GetCompo<CameraZoomController>();
        } 

        public void HoldCamera()
        {
            _cameraManager.SetFollow(transform);
            _zoomController.SetZoomLevel(_newZoomLevel, _zoomDuration, true);
        }

        public void ReleaseCamera()
        {
            _cameraManager.ResetFollow();
            _zoomController.ResetZoomLevel(_zoomDuration);
        }
    }
}