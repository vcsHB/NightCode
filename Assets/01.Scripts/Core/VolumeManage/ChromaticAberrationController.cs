    using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Core.VolumeControlSystem
{

    public class ChromaticAberrationController : VolumeController
    {
        private ChromaticAberration _chromaticEffect;

        public override void Initialize(Volume globalVolume)
        {
            globalVolume.profile.TryGet(out _chromaticEffect);

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