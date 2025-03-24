using CameraControllers;
using UnityEngine;
namespace Dialog.SituationControl
{

    public class CameraViewController : SituationElement
    {
        [SerializeField] private float _newZoomValue = 17f;
        [SerializeField] private float _duration;
        private CameraZoomController _zoomController;
        private Transform _previousFollowTarget;
        private CameraManager _cameraManager;
        void Start()
        {
            _cameraManager = CameraManager.Instance;
            _zoomController = _cameraManager.GetCompo<CameraZoomController>();
        }

        public override void StartSituation()
        {
            _previousFollowTarget = _cameraManager.CurrentFollowTarget;
            _zoomController.SetZoomLevel(_newZoomValue, _duration, true);
            _cameraManager.SetFollow(transform);
        }

        public override void EndSituation()
        {
            _cameraManager.SetFollow(_previousFollowTarget);
        }
    }
}