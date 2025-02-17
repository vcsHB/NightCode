using System.Collections;
using Core;
using UnityEngine;
namespace FeedbackSystem
{

    public class VolumeChromaticEffectFeedback : Feedback
    {
        [SerializeField] private float _level = 0.1f;
        [SerializeField] private float duration = 0.1f;
        public override void CreateFeedback()
        {
            StartCoroutine(EffectCoroutine());
        }
        private IEnumerator EffectCoroutine()
        {
            VolumeManager.Instance.SetChromatic(_level);
            yield return new WaitForSeconds(duration);
            VolumeManager.Instance.HandleDisableChromatic();
        }

        public override void FinishFeedback()
        {
            VolumeManager.Instance.HandleDisableChromatic();
        }
    }
}