using System;
using Combat;
using Combat.PlayerTagSystem;
using StatSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class CrisisSense : ChipsetFunction
    {
        [SerializeField] private int _leftCharacterAmountCondition = 1;
        [Tooltip("Unit : Percentage. 0.1 => 10%")]
        [SerializeField] private float _damageIncreasePercent = 0.1f;
        [Tooltip("Unit : Percentage. 0.1 => 10%")]
        [SerializeField] private float _healthIncreasePercent = 0.1f;

        private bool _isEnabled;
        private StatSO damageStat;
        private StatSO healthStat;
        private Health _health;
        private float _damageIncreaseValue;
        private float _healthIncreaseValue;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            enviromentData.OnCharacterAmountChangedEvent += HandlePlayerRetired;
            damageStat = _status.GetStat(StatusEnumType.Attack);
            healthStat = _status.GetStat(StatusEnumType.Health);
            _health = owner.HealthCompo;
        }

        private void HandlePlayerRetired()
        {
            if (_isEnabled) return;
            if (_environmentData.currentAliveCharacterAmount <= _leftCharacterAmountCondition)
            {
                ApplyEffects();
                _isEnabled = true;
            }
        }

        private void ApplyEffects()
        {
            _damageIncreaseValue = damageStat.Value * _damageIncreasePercent;
            _healthIncreaseValue = healthStat.Value * _healthIncreasePercent;
            damageStat.AddBuffDebuff(this, _damageIncreaseValue);
            healthStat.AddBuffDebuff(this, _healthIncreaseValue);
            _health.Restore(_healthIncreaseValue);
        }
    }
}