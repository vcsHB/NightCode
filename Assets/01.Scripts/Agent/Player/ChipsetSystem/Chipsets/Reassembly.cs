using System;
using Combat;
using Combat.PlayerTagSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class Reassembly : ChipsetFunction
    {
        private float _previouseHealth = Mathf.Infinity;
        private Health _ownerHealth;
        [SerializeField] private float _maxReturnClampValue = 40f;
        [SerializeField, Range(0f, 1f)] private float _returnRatio = 0.1f;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            _ownerHealth = owner.HealthCompo;
            _previouseHealth = _ownerHealth.CurrentHealth;
            _ownerHealth.OnHealthChangedValueEvent += HandleHealthChanged;
        }

        private void OnDestroy()
        {
            //_ownerHealth.OnHealthChangedValueEvent -= HandleHealthChanged;

        }

        private void HandleHealthChanged(float currentValue, float maxValue)
        {
            float healthDelta = _previouseHealth - currentValue;
            if (healthDelta > 0)
            {
                _previouseHealth = currentValue;
                _ownerHealth.Restore(Mathf.Clamp(healthDelta, 0f, _maxReturnClampValue) * _returnRatio);
            }

        }
    }
}