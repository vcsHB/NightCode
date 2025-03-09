using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core
{
    public enum VolumeEffectType
    {
        ChromaticAberration
    }
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _globalVolume;
        private ChromaticAberration _chromaticEffect;


        protected override void Awake()
        {
            base.Awake();
            _globalVolume.profile.TryGet(out _chromaticEffect);
        }

        public void HandleEnableChromatic()
        {
            SetChromatic(0.13f);
        }

        public void HandleDisableChromatic()
        {
            SetChromatic(0f);
        }


        public void SetChromatic(float value)
        {
            _chromaticEffect.intensity.value = value;

        }


    }
}