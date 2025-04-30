using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "AddBossHeatLevel", story: "add [HeatLevel] in [HeatController]", category: "Action", id: "3fb03e779eacbeddd43cf18397808cd9")]
    public partial class AddBossHeatLevelAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> HeatLevel;
        [SerializeReference] public BlackboardVariable<BossHeatController> HeatController;

        protected override Status OnStart()
        {
            HeatController.Value.ApplyHeat(HeatLevel);
            return Status.Success;
        }

    }


}