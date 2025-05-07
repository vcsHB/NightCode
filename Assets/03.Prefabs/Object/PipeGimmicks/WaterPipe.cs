using System;
using UnityEngine;
using UnityEngine.Events;
namespace ObjectManage.GimmickObjects
{

    public class WaterPipe : MonoBehaviour
    {
        public UnityEvent OnBlockEnableEvent;
        public UnityEvent OnBlockDsiableEvent;
        [SerializeField] private WaterHole _waterHole;
        [SerializeField] private PipeHandle _handleObject;
        [SerializeField] private float _waterBlockAngle = 360f;
        [SerializeField] private float _deadZoneAngle = 90f;
        [SerializeField] private float _rotationMinValue = 0;
        private float _currentAngle = 0.0f;
        private bool _isBlocked;
        private void Awake()
        {
            _handleObject.OnRotationEvent += HandleRotated;
        }

        private void HandleRotated(float rotationValue)
        {
            if (rotationValue < 0)
                rotationValue *= 0.1f; // 0.5 → 0.7로 완화

            _currentAngle += rotationValue;
            _currentAngle = Mathf.Clamp(_currentAngle, _rotationMinValue, _waterBlockAngle);

            if (_waterBlockAngle > _currentAngle + _deadZoneAngle)
            {
                if (_isBlocked) return;
                _isBlocked = true;
                OnBlockEnableEvent?.Invoke();
                _waterHole.SetWaterFall(true);
            }
            else if (_waterBlockAngle <= _currentAngle + _deadZoneAngle * 1.5f) // 풀림 deadzone을 1.5배 확장
            {
                if (!_isBlocked) return;
                _isBlocked = false;
                OnBlockDsiableEvent?.Invoke();
                _waterHole.SetWaterFall(false);
            }
        }

        public void HandleSetBroken()
        {
            _currentAngle = 0f;
            _waterHole.SetWaterFall(true);
        }
    }
}