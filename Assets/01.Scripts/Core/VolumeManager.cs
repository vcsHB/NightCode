using System.Collections;
using Unity.VisualScripting;
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
        private LensDistortion _lensDistortion;
        private bool _isLensEffectTweening;
        private readonly float _defaultLensDistortionLevel;


        protected override void Awake()
        {
            base.Awake();
            _globalVolume.profile.TryGet(out _chromaticEffect);
            _globalVolume.profile.TryGet(out _lensDistortion);
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

        public void SetDefaultLensDistortion()
        {
            SetLensDistortion(_defaultLensDistortionLevel);
        }
        public void SetLensDistortion(float intensity)
        {
            _lensDistortion.intensity.value = intensity;
        }
        public void SetLensDistortion(float intensity, float tweenDuration, float term)
        {
            if (_isLensEffectTweening) return;
            StartCoroutine(LensDistortionCoroutine(intensity, tweenDuration, term));
        }
        private IEnumerator LensDistortionCoroutine(float intensity, float tweenDuration, float term)
        {
            _isLensEffectTweening = true;
            float currentTime = 0f;
            float defaultValue = _lensDistortion.intensity.value;
            while (currentTime < tweenDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / tweenDuration;
                SetLensDistortion(Mathf.Lerp(defaultValue, intensity, ratio));
                yield return null;
            }
            SetLensDistortion(intensity);
            yield return new WaitForSeconds(term);
            currentTime = 0f;
            while (currentTime < tweenDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / tweenDuration;
                SetLensDistortion(Mathf.Lerp(defaultValue, intensity, 1f - ratio));
                yield return null;
            }
            SetLensDistortion(defaultValue);
            _isLensEffectTweening = false;
        }


    }
}