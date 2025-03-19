using Core;
using Core.VolumeControlSystem;
using UnityEngine;
namespace FeedbackSystem
{

    public class VolumeLensEffectFeedback : Feedback
    {
        [SerializeField] private float _intensity;
        [SerializeField] private float _tweenDuration = 0.06f;
        [SerializeField] private float _duration = 0.05f;

        private LensDistortionController _controller;
        private void Start()
        {
            _controller = VolumeManager.Instance.GetCompo<LensDistortionController>();
        }

        public override void CreateFeedback()
        {
            _controller.SetLensDistortion(_intensity, _tweenDuration, _duration);
        }

        public override void FinishFeedback()
        {
            _controller.SetDefaultLensDistortion();
        }
    }
}