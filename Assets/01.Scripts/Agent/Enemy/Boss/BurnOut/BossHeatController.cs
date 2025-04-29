using System;
using ObjectManage.GimmickObjects;
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

        private IWaterUsable _water;
        public float MaxHeatLevel => _maxHeatLevel;
        public float CurrentHeatLevel => _currentHeatLevel;


        public void ApplyHeat(float amount)
        {
            _currentHeatLevel = Mathf.Clamp(_currentHeatLevel + amount, 0, _maxHeatLevel);
            InvokeHeatLevelChangeEvent();
            OnHeatingEvent?.Invoke();
        }

        public void ApplyCooling(float amount)
        {
            _currentHeatLevel = Mathf.Clamp(_currentHeatLevel - amount, 0, _maxHeatLevel);
            InvokeHeatLevelChangeEvent();
            OnCoolingEvent?.Invoke();
        }

        public void ApplyWaterCooling(float amount)
        {
            Collider2D target = Physics2D.OverlapBox(transform.position, _waterDetectSize, 0f, _waterLayer);
            if (target == null) return;
            if (_water == null)
                if (!target.TryGetComponent(out _water))
                    return;
            _water.UseWater(amount * Time.deltaTime);
            ApplyCooling(amount);
        }

        private void InvokeHeatLevelChangeEvent()
        {
            OnHeatLevelChangeEvent?.Invoke(_currentHeatLevel, _maxHeatLevel);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, _waterDetectSize);

        }
#endif
    }
}