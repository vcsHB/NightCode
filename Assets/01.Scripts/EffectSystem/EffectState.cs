﻿using System;
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
        public float duration;

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


        public virtual void Start(int level = 1, float duration = 10f, float percent = 1f)
        {
            if (this.level < level)
                this.level = level;
            if (this.duration < duration)
                this.duration = duration;
            enabled = true;
        }

        public virtual void Update()
        {
            duration -= Time.deltaTime;
            OnUpdateEvent?.Invoke((int)duration, level);
            if(duration <= 0)
                Over();
        }

        public virtual void UpdateBySecond()
        {
            
        }

        public virtual void Over()
        {
            enabled = false;
            level = 0;
            duration = 0f;
            OnOverEvent?.Invoke();
            OnEffectOverTypeEvent?.Invoke(type);
        }


    }
}