using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace CameraControllers
{

    public class CameraZoomController : MonoBehaviour, ICameraControlable
    {
        [SerializeField] private float _defaultZoomLevel = 20f;
        [SerializeField] private float _dialogueZoomLevel = 10f;
        private CinemachineCamera _virtualCamera;
        public float ZoomLevel => _virtualCamera.Lens.OrthographicSize;
        private Coroutine _zoomCoroutine;

        public void Initialize(CinemachineCamera camera)
        {
            _virtualCamera = camera;
        }


        public void SetZoomLevel(float zoomLevel)
        {
            _virtualCamera.Lens.OrthographicSize = zoomLevel;
        }

        public void SetZoomLevel(float zoomLevel, float duration, bool isForce = false)
        {
            if (_zoomCoroutine != null)
            {
                if (!isForce) return;
                StopCoroutine(_zoomCoroutine);
            }
            _zoomCoroutine = StartCoroutine(ZoomCoroutine(zoomLevel, duration));
        }

        public void ResetZoomLevel(float duration)
        {
            SetZoomLevel(_defaultZoomLevel, duration);
        }

        private IEnumerator ZoomCoroutine(float level, float duration)
        {
            float previousLevel = ZoomLevel;
            float currentTime = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / duration;
                SetZoomLevel(Mathf.Lerp(previousLevel, level, ratio));

                yield return null;
            }
            SetZoomLevel(level);
            _zoomCoroutine = null;
        }

    }
}