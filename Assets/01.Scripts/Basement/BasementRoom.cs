using Basement.CameraController;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public BasementRoomType roomType;

        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private float _zoomInValue = 1.5f;
        private float _originZoomValue;
        private Transform _originFollow;

        public void FocusCamera()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();
            _originZoomValue = BasementCameraManager.Instance.CameraSize;
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_zoomInValue, 0.4f);
        }

        public void ReturnFocus()
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_originZoomValue, 0.4f);
        }
    }
}
