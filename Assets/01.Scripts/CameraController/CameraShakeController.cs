using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace CameraControllers
{

    public class CameraShakeController : MonoBehaviour, ICameraControlable
    {
        private CinemachineCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _shaker;
        private bool _isShaking;


        public void Initialize(CinemachineCamera camera)
        {
            _virtualCamera = camera;
            _shaker = _virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        }
        public void HandleSmallShake()
        {
            Shake(10f, 0.07f);
        }

        public void HandleMiddleShake()
        {
            Shake(20f, 0.07f);
        }

        public void Shake(float power, float duration)
        {
            if (_isShaking) return;
            _isShaking = true;
            StartCoroutine(ShakeCoroutine(power, duration));
        }

        private IEnumerator ShakeCoroutine(float power, float duration)
        {
            SetShake(power);
            yield return new WaitForSeconds(duration);
            SetShake(0);
            _isShaking = false;
        }

        public void StopShake()
        {
            _isShaking = false;

        }

        private void SetShake(float power)
        {
            _shaker.AmplitudeGain = power;
            _shaker.FrequencyGain = power;
        }

    }

}