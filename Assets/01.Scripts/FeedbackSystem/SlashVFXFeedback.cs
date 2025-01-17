using ObjectManage;
using UnityEngine;

namespace FeedbackSystem
{

    public class SlashVFXFeedback : Feedback
    {
        [SerializeField] private SlashVFXPlayer _slashVFX;
        public override void CreateFeedback()
        {
            _slashVFX.Play();
        }

        public override void FinishFeedback()
        {

        }
    }

}