using SoundManage;
using UnityEngine;

namespace FeedbackSystem
{
    public class SoundFeedback : Feedback
    {
        [SerializeField] private SoundSO _soundData;

        public override void CreateFeedback()
        {
            SoundController.Instance.PlaySound(_soundData, transform.position);
        }

        public override void FinishFeedback()
        {
        }
    }
}