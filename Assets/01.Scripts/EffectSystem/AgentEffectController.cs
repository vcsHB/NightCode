using System;
using System.Collections.Generic;
using Agents;
using UnityEngine;

namespace EffectSystem
{
    public class AgentEffectController : MonoBehaviour, IEffectable, IAgentComponent
    {
        public event Action<EffectStateTypeEnum> OnEffectStartEvent;
        public event Action<EffectStateTypeEnum> OnEffectOverEvent;
        public Dictionary<EffectStateTypeEnum, EffectState> effectDictionary = new Dictionary<EffectStateTypeEnum, EffectState>();
        protected Agent _owner;
        protected float _currentTime = 0f;

        public virtual void Initialize(Agent agent)
        {
            _owner = agent;
            _owner.OnDieEvent += HandleOwnerDie;
        }

        private void HandleOwnerDie()
        {
            foreach (var effect in effectDictionary)
            {
                effect.Value.ResetEffectStack();
                OnEffectOverEvent?.Invoke(effect.Value.EffectType);
            }
        }

        public void AfterInit() { }

        public void Dispose() { }


        private void Start()
        {
            InitializeEffects();

        }

        private void InitializeEffects()
        {
            EffectState[] effects = GetComponentsInChildren<EffectState>();
            foreach (EffectState effect in effects)
            {
                effect.SetEffectType();
                if (effect.EffectType == EffectStateTypeEnum.None) return;

                if (effectDictionary.TryAdd(effect.EffectType, effect))
                {
                    effect.Initialize(_owner, false);
                    effect.OnEffectOverTypeEvent += HandleEffectOver;

                }
            }

        }

        private void HandleEffectOver(EffectStateTypeEnum type)
        {
            OnEffectOverEvent?.Invoke(type);
        }

        protected virtual void Update()
        {
            _currentTime += Time.deltaTime;
            bool isOneSecond = _currentTime > 1f;
            foreach (EffectState effect in effectDictionary.Values)
            {
                if (effect.isEffectEnabled)
                {
                    if (isOneSecond)
                        effect.UpdateBySecond();
                }
            }
            if (isOneSecond)
                _currentTime = 0f;
        }


        /**
         * <param name="type">
         * 효과 타입 enum
         * </param>
         * <param name="duration">
         * 효과 지속시간
         * </param>
         * <param name="level">
         * 효과 강도
         * </param>
         * <summary>
         * 효과 부여 메서드
         * </summary>
         */
        public virtual void ApplyEffect(EffectStateTypeEnum type, int level, int stack, float percent = 1f)
        {
            if (effectDictionary.TryGetValue(type, out EffectState effect))
            {
                effect.Apply(level, stack, percent);
                OnEffectStartEvent?.Invoke(type);
            }
        }

        public virtual void RemoveEffect(EffectStateTypeEnum type)
        {
            if (effectDictionary.TryGetValue(type, out EffectState effect))
            {
                effect.Over();
                OnEffectOverEvent?.Invoke(type);
            }
        }

        public EffectState GetEffectState(EffectStateTypeEnum type)
        {
            return effectDictionary[type];
        }


    }
}