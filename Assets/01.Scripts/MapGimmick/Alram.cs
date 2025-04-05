using Agents.Enemies;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace Ingame.Gimmick
{
    public class Alram : MonoBehaviour
    {
        [SerializeField] private EnemySpawnGroup _spawnGroup;

        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _maxIntencity;
        [SerializeField] private float _blinkSpeed;
        [SerializeField] private Transform _lightTrm;

        private Light2D[] _lights;
        private bool _isRinging = false;
        private float _timer;

        private void Awake()
        {
            _lights = _lightTrm.GetComponentsInChildren<Light2D>();
        }

        private void Update()
        {
            if (_isRinging)
            {
                _lightTrm.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));
                _timer += Time.deltaTime * _blinkSpeed;

                float intencity = (Mathf.Sin(_timer) + 1) / 2 * _maxIntencity;
                foreach (var light in _lights)
                {
                    light.intensity = intencity;
                }
            }
        }

        public void StopRinging()
        {
            _isRinging = false;

            foreach (var light in _lights)
                light.intensity = 0;
        }

        public void StartRining()
        {
            if (_isRinging) return;
            _isRinging = true;
            _timer = 0;

            _spawnGroup.StartWave();
            //적 스폰하기
        }
    }
}
