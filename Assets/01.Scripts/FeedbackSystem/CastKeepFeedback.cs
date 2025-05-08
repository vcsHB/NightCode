using Combat.Casters;
using UnityEngine;
namespace FeedbackSystem
{

    public class CastKeepFeedback : Feedback
    {
        [SerializeField] private Caster _caster;
        private bool _isCasting = false;

        private void Update()
        {
            if (_isCasting)
                _caster.Cast();
        }
        public override void CreateFeedback()
        {
            _isCasting = true;
        }

        public override void FinishFeedback()
        {
            _isCasting = false;
        }
    }
}