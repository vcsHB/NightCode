using CameraControllers;
using System;
using System.Collections;
using UnityEngine;

namespace Base
{
    public class CameraFocuser : MonoBehaviour
    {
        public event Action onCompleteFocus;
        public event Action onCompleteResetFocus;

        [SerializeField] private bool _zoomCamera;
        [SerializeField] private float _zoomValue, _zoomDuration;
        public float actionDelay;
        private CameraZoomController _cameraZoomController;

        private void Start()
        {
            _cameraZoomController = CameraManager.Instance.GetCompo<CameraZoomController>();
        }

        public void SetFocus()
        {
            CameraManager.Instance.SetFollow(transform);
            StartCoroutine(DelayInvokeFocus());
            if (_zoomCamera) _cameraZoomController.SetZoomLevel(_zoomValue, _zoomDuration);
        }

        public void ResetFocus()
        {
            CameraManager.Instance.ResetFollow();
            StartCoroutine(DelayInvokeResetFocus());
            if (_zoomCamera) _cameraZoomController.ResetZoomLevel(_zoomDuration);
        }

        private IEnumerator DelayInvokeFocus()
        {
            yield return new WaitForSeconds(_zoomDuration + actionDelay);
            onCompleteFocus?.Invoke();
        }

        private IEnumerator DelayInvokeResetFocus()
        {
            yield return new WaitForSeconds(_zoomDuration + actionDelay);
            onCompleteResetFocus?.Invoke();
        }
    }
}
