using UnityEngine;
namespace FeedbackSystem
{
    public class VFXFeedback : Feedback
    {
        [SerializeField] private ParticleSystem _particle;

        public override void CreateFeedback()
        {
            _particle.Play();
        }

        public override void FinishFeedback()
        {
            _particle.Stop();

        }
    }
}