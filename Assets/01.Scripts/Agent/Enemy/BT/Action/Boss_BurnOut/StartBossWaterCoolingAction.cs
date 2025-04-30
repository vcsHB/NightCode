using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StartBossWaterCooling", story: "start water cooling is [Power] multiplier by [HeatController] for [Duration]", category: "Action", id: "43149587fe62dc2704810d167e67b255")]
    public partial class StartBossWaterCoolingAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> Power;
        [SerializeReference] public BlackboardVariable<BossHeatController> HeatController;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private float _startTime;
        protected override Status OnStart()
        {
            _startTime = Time.time;

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            HeatController.Value.ApplyWaterCooling(Power.Value);
            if (_startTime + Duration.Value < Time.time)
            {
                return Status.Success;
            }
            return Status.Running;
        }

    }
}