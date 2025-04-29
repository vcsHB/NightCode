using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StartBossCooling", story: "start cooling [Amount] by [HeatController]", category: "Action", id: "a99578569a8177c3e9a42eaf53c7c4f2")]
    public partial class StartBossCoolingAction : Action
    {
        [SerializeReference] public BlackboardVariable<int> Amount;
        [SerializeReference] public BlackboardVariable<BossHeatController> HeatController;

        protected override Status OnStart()
        {
            HeatController.Value.ApplyCooling(Amount.Value);
            return Status.Success;
        }

    }


}