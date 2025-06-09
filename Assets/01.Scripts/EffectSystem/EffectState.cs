using System;
using Agents;
using Combat;
using UnityEngine;

namespace EffectSystem
{
    public class EffectState
    {
        public event Action<int, int> OnUpdateEvent;
        public event Action OnOverEvent;
        public event Action<EffectStateTypeEnum> OnEffectOverTypeEvent;
        public bool isResist;
        public bool enabled;
        public int level;
        public int currentEffectStack;
        public int stackBurstConditionLevel;

        public EffectStateTypeEnum type;
        protected Agent _owner;
        protected Health _ownerHealth;
        protected Transform _ownerTrm;

        public EffectState(Agent agent, bool isResist)
        {
            _owner = agent;
            _ownerHealth = _owner.HealthCompo;
            _ownerTrm = _owner.transform;
            this.isResist = isResist;
        }


        public virtual void Start(int stack = 1, int level = 1, float percent = 1f)
        {
            if (this.level < level)
                this.level = level;

            if (stack > stackBurstConditionLevel)
            {
                ResetEffectStack();
                OnBurstEffectStack();

            }
            enabled = true;
        }

        public virtual void UpdateBySecond()
        {

        }

        protected virtual void OnBurstEffectStack()
        {

        }

        protected virtual void ResetEffectStack()
        {
            level = 0;
            currentEffectStack = 0;

        }

        public virtual void Over()
        {
            enabled = false;
            OnOverEvent?.Invoke();
            OnEffectOverTypeEvent?.Invoke(type);
        }


    }
}