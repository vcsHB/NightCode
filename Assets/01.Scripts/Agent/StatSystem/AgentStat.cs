using GGM.Core.StatSystem;
using System.Linq;
using UnityEngine;

namespace Agents
{
    public class AgentStat : MonoBehaviour//, IEntityComponent
    {
        [SerializeField] private StatOverride[] _statOverrides;
        private StatSO[] _stats;
        private Agent _owner;

        public void Initialize(Agent agent)
        {
            _owner = agent;
            _stats = _statOverrides.Select(x => x.CreateStat()).ToArray();
        }

        public StatSO GetStat(StatSO stat)
        {
            Debug.Assert(stat != null, "Stat : GetStat - stat cannot be null");
            return _stats.FirstOrDefault(x => x.statName == stat.statName);
        }

        public bool TryGetStat(StatSO stat, out StatSO outStat)
        {
            Debug.Assert(stat != null, "TryGetStat : GetStat - stat cannot be null");
            outStat = _stats.FirstOrDefault(x => x.statName == stat.statName);
            return outStat != null;
        }

        public void SetBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue = value;

        public float GetBaseValue(StatSO stat)
            => GetStat(stat).BaseValue;

        public float IncreaseBaseValue(StatSO stat, float value)
            => GetStat(stat).BaseValue += value;


        public void AddModifier(StatSO stat, object key, float value)
            => GetStat(stat).AddModifier(key, value);


        public void RemoveModifier(StatSO stat, object key)
            => GetStat(stat).RemoveModifier(key);

        public void ClearAllModifiers()
        {
            foreach (StatSO stat in _stats)
            {
                stat.ClearModifiers();
            }
        }
    }
}
