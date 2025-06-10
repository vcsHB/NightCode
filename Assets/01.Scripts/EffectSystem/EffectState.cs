using System;
using Agents;
using Combat;
using UnityEngine;

namespace EffectSystem
{
    public abstract class EffectState : MonoBehaviour
    {
        public event Action<int, int> OnUpdateEvent;
        public event Action OnOverEvent;
        public event Action<EffectStateTypeEnum> OnEffectOverTypeEvent;
        public bool isResist;
        public bool isEffectEnabled;
        public int level;
        public int currentEffectStack;
        public int stackBurstConditionLevel;
        public EffectStateTypeEnum EffectType { get; protected set; }

        public EffectStateTypeEnum type;
        protected Agent _owner;
        protected Health _ownerHealth;
        protected Transform _ownerTrm;

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetEffectType();
        }
#endif
        public virtual void Initialize(Agent agent, bool isResist)
        {
            _owner = agent;
            _ownerHealth = _owner.HealthCompo;
            _ownerTrm = _owner.transform;
            this.isResist = isResist;
        }

        protected abstract void SetEffectType();


        public virtual void Apply(int stack = 1, int level = 1, float percent = 1f)
        {
            if (this.level < level)
                this.level = level;

            if (stack > stackBurstConditionLevel)
            {
                ResetEffectStack();
                OnBurstEffectStack();

            }
            isEffectEnabled = true;
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
            isEffectEnabled = false;
            OnOverEvent?.Invoke();
            OnEffectOverTypeEvent?.Invoke(type);
        }

        protected void ReduceStack(int amount = 1)
        {
            currentEffectStack -= amount;
        }


    }
}