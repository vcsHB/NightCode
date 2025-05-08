using SoundManage;
using UnityEngine;

namespace FeedbackSystem
{
    public class SoundFeedback : Feedback
    {
        [SerializeField] private SoundSO _soundData;
        private SoundPlayer _soundPlayer;
        public override void CreateFeedback()
        {
            _soundPlayer = SoundController.Instance.PlaySound(_soundData, transform.position);

        }

        public override void FinishFeedback()
        {
            if (_soundData.loop)
            {
                _soundPlayer.SetForceOverSound();
            }
        }
    }
}