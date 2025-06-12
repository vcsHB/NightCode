using Core.VolumeControlSystem;
using UnityEngine;
namespace FeedbackSystem
{

    public class ColorAdjustmentEffectFeedback : Feedback
    {
        [SerializeField] private float _effectHueValue;
        [SerializeField] private float _effectSaturationValue;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private float _tweenDuration = 0.1f;
        private ColorAdjustmentController _colorAdjuestmentController;
        private void Start()
        {

            _colorAdjuestmentController = VolumeManager.Instance.GetCompo<ColorAdjustmentController>();
        }
        public override void CreateFeedback()
        {
            _colorAdjuestmentController.StartEffectSchedule(_effectHueValue, _effectSaturationValue, _tweenDuration, _duration);

        }

        public override void FinishFeedback()
        {
            _colorAdjuestmentController.EndEffectSchedule();

        }
    }
}