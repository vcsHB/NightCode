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
            Initialize();
        }

        public void AfterInit() { }

        public void Dispose() { }

        private void Initialize()
        {
            EffectState[] effects = GetComponentsInChildren<EffectState>();
            foreach (EffectState item in effects)
            {
                
            }
            foreach (EffectStateTypeEnum effectEnum in Enum.GetValues(typeof(EffectStateTypeEnum)))
            {
                if (effectEnum == 0) continue;

                string typeName = effectEnum.ToString();
                Type t = Type.GetType($"EffectSystem.Effect{typeName}");

                try
                {
                    EffectState effect = Activator.CreateInstance(t, _owner, false) as EffectState;
                    effect.OnEffectOverTypeEvent += HandleEffectOver;
                    effectDictionary.Add(effectEnum, effect);
                    effect.type = effectEnum;

                }
                catch (Exception ex)
                {
                    Debug.LogError($"Effect Controller : no Effect found [ {typeName} ] - {ex.Message}");
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
            effectDictionary[type].Apply(level, stack, percent);
            OnEffectStartEvent?.Invoke(type);
        }

        public virtual void RemoveEffect(EffectStateTypeEnum type)
        {
            effectDictionary[type].Over();
            OnEffectOverEvent?.Invoke(type);
        }

        public EffectState GetEffectState(EffectStateTypeEnum type)
        {
            return effectDictionary[type];
        }


    }
}