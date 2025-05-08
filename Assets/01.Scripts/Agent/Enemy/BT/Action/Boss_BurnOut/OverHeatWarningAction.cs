using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "OverHeatWarning", story: "[Boss] warning for [Duration]", category: "Action", id: "ec7b1e17811c5dbc5cdf8a897b7e5d1a")]
    public partial class OverHeatWarningAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutBoss> Boss;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private FeedbackCreateEventData _createFeedbackData;
        private FeedbackFinishEventData _finishFeedbackData;
        private float _startTime;

        protected override Status OnStart()
        {
            if (_createFeedbackData == null)
                _createFeedbackData = new FeedbackCreateEventData("SteamCooling");
            _startTime = Time.time;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }


    }



}