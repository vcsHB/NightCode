using System.Collections;
using Combat.Casters.CastingVisuals;
using UnityEngine;
namespace FeedbackSystem
{

    public class CastRangeBlinkFeedback : Feedback
    {
        [SerializeField] private CastWarningVisual _castWarningVisual;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _blinkCurve;
        [SerializeField] private float _blinkFrequency = 10f;
        public override void CreateFeedback()
        {
            StartCoroutine(BlinkCoroutine());
        }
        private IEnumerator BlinkCoroutine()
        {
            float currentTime = 0f;
            bool isVisible = false;

            while (currentTime < _duration)
            {
                float progress = currentTime / _duration;
                float curveValue = _blinkCurve.Evaluate(progress); // Get Speed in Curve
                float currentFrequency = Mathf.Lerp(_blinkFrequency, _blinkFrequency * 5f, curveValue); // 예: 최대 5배 빠르게
                float blinkInterval = 1f / currentFrequency;
                currentTime += blinkInterval;

                isVisible = !isVisible;
                _castWarningVisual.SetActiveVisual(isVisible);
                yield return new WaitForSeconds(blinkInterval);

            }
            _castWarningVisual.SetActiveVisual(false);
        }

        public override void FinishFeedback()
        {
        }
    }
}