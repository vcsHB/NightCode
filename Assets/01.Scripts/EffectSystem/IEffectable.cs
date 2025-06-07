﻿namespace EffectSystem
{
    public interface IEffectable
    {
        public void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f);
    }
}