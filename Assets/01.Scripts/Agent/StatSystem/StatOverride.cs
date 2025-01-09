using System;
using UnityEngine;

namespace GGM.Core.StatSystem
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSO _stat;
        [SerializeField] private bool _isUseOverride;
        [SerializeField] private float _overrideBaseValue;

        public StatOverride(StatSO stat)
        {
            _stat = stat;
        }

        public StatSO CreateStat()
        {
            StatSO newStat = _stat.Clone() as StatSO;
            Debug.Assert(newStat != null, $"newStat is null check name {_stat.statName}");

            if (_isUseOverride)
                newStat.BaseValue = _overrideBaseValue;

            return newStat;
        }
    }
}
