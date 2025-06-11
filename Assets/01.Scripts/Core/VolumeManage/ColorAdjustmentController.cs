using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Core.VolumeControlSystem
{

    public class ColorAdjustmentController : VolumeController
    {
        private ColorAdjustments _colorAdjustment;
        private bool _isColorAdjustmentTweening;
        private float _defaultHueLevel;
        private float _defaultSaturationLevel;
        private Coroutine _currentSceduleRoutine;
        public override void Initialize(Volume globalVolume)
        {
            globalVolume.profile.TryGet(out _colorAdjustment);
            if (_colorAdjustment == null)
            {
                Debug.LogError("[ColorAdjustmentController] Can't Find Component");
                return;
            }
            _defaultHueLevel = _colorAdjustment.hueShift.value;
            _defaultSaturationLevel = _colorAdjustment.saturation.value;

        }

        public void SetColorAdjustment(float hue, float saturation)
        {
            _colorAdjustment.hueShift.value = hue;
            _colorAdjustment.saturation.value = saturation;
        }

        public void SetColorAdjustment(float hue, float saturation, float duration)
        {
            if (_isColorAdjustmentTweening) return;
            StartCoroutine(ColorAdjustmentEffectCoroutine(hue, saturation, duration));
        }

        #region  Effect Schedule Manage
        public void StartEffectSchedule(float hue, float saturation, float tweenDuration, float duration)
        {
            if (_isColorAdjustmentTweening) return;
            _currentSceduleRoutine = StartCoroutine(EffectScheduleCoroutine(hue, saturation, tweenDuration, duration));
        }
        public void EndEffectSchedule()
        {
            if (_currentSceduleRoutine == null) return;
            StopCoroutine(_currentSceduleRoutine);
            SetColorAdjustment(_defaultHueLevel, _defaultSaturationLevel);
            _isColorAdjustmentTweening = false;
        }

        public IEnumerator EffectScheduleCoroutine(float hue, float saturation, float tweenDuration, float duration)
        {
            yield return StartCoroutine(ColorAdjustmentEffectCoroutine(hue, saturation, tweenDuration));
            _isColorAdjustmentTweening = true;
            yield return new WaitForSeconds(duration);
            yield return StartCoroutine(ColorAdjustmentEffectCoroutine(_defaultHueLevel, _defaultSaturationLevel, tweenDuration));
            _isColorAdjustmentTweening = false;
            _currentSceduleRoutine = null;
        }

        #endregion

        private IEnumerator ColorAdjustmentEffectCoroutine(float hue, float saturation, float duration)
        {
            float currentTime = 0f;
            float defaultHue = _colorAdjustment.hueShift.value;
            float defualtSaturation = _colorAdjustment.saturation.value;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / duration;
                SetColorAdjustment(Mathf.Lerp(defaultHue, hue, ratio), Mathf.Lerp(defualtSaturation, saturation, ratio));
                yield return null;
            }
            SetColorAdjustment(hue, saturation);
            _isColorAdjustmentTweening = false;
        }
    }
}