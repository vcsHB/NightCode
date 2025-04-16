using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/Stat/StatSO")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValuechangeHandler(StatSO stat, float currentValue, float prevValue);

        public event ValuechangeHandler OnValuechange;
        public StatusEnumType statType;
        public string statName;
        public string description;
        public string displayName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        public float buffDebuffValue = 0;       //UI 만들 때 구분할 수 있게

        private Dictionary<object, float> _modifyValueByKey = new Dictionary<object, float>();
        private List<float> _modifyValue = new List<float>();

        //[field: SerializeField] public bool IsPercent { get; private set; }

        public Sprite Icon => _icon;

        public float MaxValue => _maxValue;
        public float MinValue => _minValue;

        public float Value => Mathf.Clamp(_baseValue + buffDebuffValue, _minValue, _maxValue);
        public bool IsMax => Mathf.Approximately(Value, _maxValue);
        public bool IsMin => Mathf.Approximately(Value, _minValue);

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                float prevValue = Value;
                _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void AddBuffDebuff(object key, float value)
        {
            float prevValue = Value;
            buffDebuffValue += value;

            if (_modifyValueByKey.ContainsKey(key))
            {
                _modifyValueByKey[key] += value;
                TryInvokeValueChangeEvent(Value, prevValue);
                return;
            }


            _modifyValueByKey.Add(key, value);
            TryInvokeValueChangeEvent(Value, prevValue);
        }

        public void RemovedBuffDebuff(object key)
        {
            if (_modifyValueByKey.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                buffDebuffValue -= value;
                _modifyValueByKey.Remove(key);
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void AddModifier(float value)
        {
            BaseValue += value;
            _modifyValue.Add(value);
        }

        public void RemoveModifier(float value)
        {
            if (_modifyValue.Contains(value) == false)
            {
                Debug.LogWarning($"There is no modify but you try to remove it");
                return;
            }
            BaseValue -= value;
            _modifyValue.Remove(value);
        }

        public void ClearBuffDebuff()
        {
            float prevValue = Value;
            _modifyValueByKey.Clear();
            buffDebuffValue = 0;
            TryInvokeValueChangeEvent(Value, prevValue);
        }

        private void TryInvokeValueChangeEvent(float currentValue, float prevValue)
        {
            if (Mathf.Approximately(currentValue, prevValue) == false)
            {
                OnValuechange?.Invoke(this, currentValue, prevValue);
            }
        }

        public virtual object Clone() => ScriptableObject.Instantiate(this);
    }
}
