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

        public abstract void SetEffectType();


        public virtual void Apply(int stack = 1, int level = 1, float percent = 1f)
        {
            if (this.level < level)
                this.level = level;

            currentEffectStack += stack;
            if (currentEffectStack >= stackBurstConditionLevel)
            {
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

        public virtual void ResetEffectStack()
        {
            level = 0;
            currentEffectStack = 0;
            isEffectEnabled = false;
        }

        public virtual void Over()
        {
            isEffectEnabled = false;
            ResetEffectStack();
            OnOverEvent?.Invoke();
            OnEffectOverTypeEvent?.Invoke(EffectType);
        }

        protected virtual void ReduceStack(int amount = 1)
        {
            currentEffectStack -= amount;
        }
        
        //protected virtual void 


    }
}