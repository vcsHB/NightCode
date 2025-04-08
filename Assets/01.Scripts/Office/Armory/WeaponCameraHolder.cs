using CameraControllers;
using UnityEngine;
namespace Office.Armory
{

    public class WeaponCameraHolder : MonoBehaviour
    {
        [SerializeField] private float _newZoomLevel = 12f;
        [SerializeField] private float _zoomDuration = 1f;
        [SerializeField] private Vector2 _cameraOffset;
        private CameraManager _cameraManager;
        private CameraZoomController _zoomController;
        private void Start()
        {
            _cameraManager = CameraManager.Instance;
            _zoomController = _cameraManager.GetCompo<CameraZoomController>();
        } 

        public void HoldCamera()
        {
            _cameraManager.SetFollow(transform);
            _zoomController.SetZoomLevel(_newZoomLevel, _zoomDuration, true);
            _cameraManager.SetFollowOffset(_cameraOffset);
        }

        public void ReleaseCamera()
        {
            _cameraManager.ResetFollow();
            _zoomController.ResetZoomLevel(_zoomDuration);
            _cameraManager.ResetFollowOffset();
            
        }
    }
}