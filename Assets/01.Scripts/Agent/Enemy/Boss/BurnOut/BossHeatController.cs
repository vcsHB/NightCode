using System;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.BossManage
{

    public class BossHeatController : MonoBehaviour
    {
        public UnityEvent OnHeatingEvent;
        public UnityEvent OnCoolingEvent;
        public event Action<float, float> OnHeatLevelChangeEvent;
        [SerializeField] private float _maxHeatLevel;
        [SerializeField] private LayerMask _waterLayer;
        [SerializeField] private Vector2 _waterDetectSize;
        [SerializeField] private float _currentHeatLevel;

        public float MaxHeatLevel => _maxHeatLevel;
        public float CurrentHeatLevel => _currentHeatLevel;


        public void AddHeat(float amount)
        {
            _currentHeatLevel = Mathf.Clamp(_currentHeatLevel + amount, 0, _maxHeatLevel);
            InvokeHeatLevelChangeEvent();
            OnHeatingEvent?.Invoke();
        }

        public void SetCooling(float amount)
        {
            _currentHeatLevel = Mathf.Clamp(_currentHeatLevel - amount, 0, _maxHeatLevel);
            InvokeHeatLevelChangeEvent();
            OnCoolingEvent?.Invoke();
        }

        private void InvokeHeatLevelChangeEvent()
        {
            OnHeatLevelChangeEvent?.Invoke(_currentHeatLevel, _maxHeatLevel);
        }
    }
}