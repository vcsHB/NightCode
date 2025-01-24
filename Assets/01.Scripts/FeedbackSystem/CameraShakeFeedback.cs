using CameraControllers;
using UnityEngine;
namespace FeedbackSystem
{
    public class CameraShakeFeedback : Feedback
    {
        [SerializeField] private float _shakePower = 10f;
        [SerializeField] private float _shakeDuration = 0.2f;
        private CameraShakeController _cameraShaker;

        private void Start()
        {
            _cameraShaker = CameraManager.Instance.GetCompo<CameraShakeController>();
        }

        public override void CreateFeedback()
        {
            _cameraShaker.Shake(_shakePower, _shakeDuration);
        }


        public override void FinishFeedback()
        {
            _cameraShaker.StopShake();
        }
    }
}