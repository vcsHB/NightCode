using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGM.Core.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSO")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValuechangeHandler(StatSO stat, float currentValue, float prevValue);

        public event ValuechangeHandler OnValuechange;

        public string statName;
        public string description;
        public string displayName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        public float modifyValue = 0;

        private Dictionary<object, float> _modifyValueByKey = new Dictionary<object, float>();

        //[field: SerializeField] public bool IsPercent { get; private set; }

        public Sprite Icon => _icon;

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        public float Value => Mathf.Clamp(_baseValue + modifyValue, _minValue, _maxValue);
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

        public void AddModifier(object key, float value)
        {
            float prevValue = Value;
            modifyValue += value;

            if (_modifyValueByKey.ContainsKey(key))
            {
                _modifyValueByKey[key] += value;
                TryInvokeValueChangeEvent(Value, prevValue);
                return;
            }


            _modifyValueByKey.Add(key, value);
            TryInvokeValueChangeEvent(Value, prevValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyValueByKey.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                modifyValue -= value;
                _modifyValueByKey.Remove(key);
                TryInvokeValueChangeEvent(Value, prevValue);
            }
        }

        public void ClearModifiers()
        {
            float prevValue = Value;
            _modifyValueByKey.Clear();
            modifyValue = 0;
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
