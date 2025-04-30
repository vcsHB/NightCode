using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "FinishFeedback", story: "finish [Agent] [FeedbackName] feedback", category: "Action", id: "9b927d236b6f8c1f567e568d8c63f09e")]
    public partial class FinishFeedbackAction : Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Agent;
        [SerializeReference] public BlackboardVariable<string> FeedbackName;
        private FeedbackFinishEventData _finishFeedbackData;

        protected override Status OnStart()
        {
            if (_finishFeedbackData == null)
                _finishFeedbackData = new FeedbackFinishEventData(FeedbackName.Value);
            Agent.Value.EventChannel.RaiseEvent(_finishFeedbackData);

            return Status.Running;
        }


    }


}