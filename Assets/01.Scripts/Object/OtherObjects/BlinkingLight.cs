using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace ObjectManage
{
    public struct BlinkData
    {
        public float blinkPlayTerm;
        public float intensity;
        public float duration;

    }
    public class BlinkingLight : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private bool _isLoop;
        [SerializeField] private BlinkData[] _blinkDatas;
        private int _currentBlinkIndex;
        [SerializeField] private bool _playOnAwake;
        private bool _isActive;
        private float _lastBlinkTime;
        private float _currentPlayTime;

        private void Awake()
        {
            if (_playOnAwake)
                Play();
        }

        public void Play()
        {
            _isActive = true;
        }


        private void Update()
        {
            if (!_isActive) return;
            if (_lastBlinkTime + _blinkDatas[_currentBlinkIndex].blinkPlayTerm > Time.deltaTime)
            {
                SetLightIntensity(_blinkDatas[_currentBlinkIndex].intensity);
                _lastBlinkTime = Time.time;
            }
        }

        private void SetLightIntensity(float intensity)
        {
            _light.intensity = intensity;
        }

    }
}