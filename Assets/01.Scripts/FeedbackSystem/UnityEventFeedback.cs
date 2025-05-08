    using UnityEngine;
using UnityEngine.Events;
namespace FeedbackSystem
{

    public class UnityEventFeedback : Feedback
    {
        public UnityEvent OnCreateEvent;
        public UnityEvent OnFinishEvent;
        public override void CreateFeedback()
        {
            OnCreateEvent?.Invoke();
        }

        public override void FinishFeedback()
        {
            OnFinishEvent?.Invoke();
        }
    }
}