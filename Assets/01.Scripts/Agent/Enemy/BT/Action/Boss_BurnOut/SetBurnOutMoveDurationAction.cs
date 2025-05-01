using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetBurnOutMoveDuration", story: "set [Mover] duration [Value]", category: "Action", id: "870d4faeda9f9acb122ba92e5f3930a1")]
    public partial class SetBurnOutMoveDurationAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutBossMovement> Mover;
        [SerializeReference] public BlackboardVariable<float> Value; // 

        protected override Status OnStart()
        {
            Mover.Value.SetAxisDuration(Value.Value);
            return Status.Success;
        }

    }


}