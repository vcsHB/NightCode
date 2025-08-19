using System.Collections.Generic;
using Agents;
using Agents.Players;
using UnityEngine;
using static StatSystem.StatSO;
namespace StatSystem
{

    public class AgentStatus : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private StatOverride[] _statOverrides;
        private Dictionary<StatusEnumType, StatSO> _statDictionary = new();
        private Agent _owner;


        public void Initialize(Agent agent)
        {
            _owner = agent;
            InitializeStatDictionary();

        }
        public void AfterInit()
        {
            if (_owner is Player player)
                player.OnPlayerGenerateEvent += InitializeStatUsables;

        }

        private void InitializeStatUsables()
        {
            IStatUsable[] statUsables = _owner.transform.GetComponentsInChildren<IStatUsable>();

            for (int i = 0; i < statUsables.Length; i++)
            {
                statUsables[i].Initialize(this);
            }
        }

        public void Dispose() { }

        private void InitializeStatDictionary()
        {
            foreach (StatOverride statOverride in _statOverrides)
            {
                StatSO newStat = statOverride.CreateStat();
                if (!_statDictionary.TryAdd(newStat.statType, newStat))
                {
                    Debug.LogError($"This Key is already included in statDictionary! [statType:{newStat.statType}]");
                }
            }
        }

        public StatSO GetStat(StatusEnumType type)
        {
            if (_statDictionary.TryGetValue(type, out StatSO stat))
            {
                return stat;
            }
            Debug.LogError($"Not Exist Status Type in dictionary {type.ToString()}");
            return null;
        }

        public void AddListener(StatusEnumType type, ValuechangeHandler action)
        {
            if (_statDictionary.TryGetValue(type, out StatSO stat))
            {
                stat.OnValuechange += action;
            }
        }

        public void RemoveListener(StatusEnumType type, ValuechangeHandler action)
        {

            if (_statDictionary.TryGetValue(type, out StatSO stat))
            {
                stat.OnValuechange -= action;
            }
        }

        public void SetBaseValue(StatusEnumType statType, float value)
            => GetStat(statType).BaseValue = value;

        public float GetBaseValue(StatusEnumType statType)
            => GetStat(statType).BaseValue;

        public float IncreaseBaseValue(StatusEnumType statType, float value)
            => GetStat(statType).BaseValue += value;


        public void AddModifier(StatusEnumType statType, object key, float value)
            => GetStat(statType).AddBuffDebuff(key, value);


        public void RemoveModifier(StatusEnumType statType, object key)
            => GetStat(statType).RemovedBuffDebuff(key);
    }
}