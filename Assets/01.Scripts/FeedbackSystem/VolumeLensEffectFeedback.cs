using Core;
using UnityEngine;
namespace FeedbackSystem
{

    public class VolumeLensEffectFeedback : Feedback
    {
        [SerializeField] private float _intensity;
        [SerializeField] private float _tweenDuration = 0.06f;
        [SerializeField] private float _duration = 0.05f;

        public override void CreateFeedback()
        {
            VolumeManager.Instance.SetLensDistortion(_intensity, _tweenDuration, _duration);
        }

        public override void FinishFeedback()
        {
            VolumeManager.Instance.SetDefaultLensDistortion();
        }
    }
}