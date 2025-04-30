using Agents;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "InvokeFeedback", story: "invoke [Agent] [FeedbackName] feedback", category: "Action", id: "f43d178327f35b4268229223ed299966")]
    public partial class InvokeFeedbackAction : Action
    {
        [SerializeReference] public BlackboardVariable<Agent> Agent;
        [SerializeReference] public BlackboardVariable<string> FeedbackName;
        private FeedbackCreateEventData _createFeedbackData;

        protected override Status OnStart()
        {
            if (_createFeedbackData == null)
                _createFeedbackData = new FeedbackCreateEventData(FeedbackName.Value);
            Agent.Value.EventChannel.RaiseEvent(_createFeedbackData);
            return Status.Running;
        }

    }


}