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
            _cameraShaker = CameraControllers.CameraManager.Instance.GetCompo<CameraShakeController>();
        }

        public override void CreateFeedback()
        {
            if(_cameraShaker == null)
            {
                Debug.LogWarning("CameraShaker is Null!");
                return;
            }
            _cameraShaker?.Shake(_shakePower, _shakeDuration);
        }


        public override void FinishFeedback()
        {
            if(_cameraShaker == null)
            {
                Debug.LogWarning("CameraShaker is Null!");
                return;
            }
            _cameraShaker?.CancelCurrentShake();
        }
    }
}