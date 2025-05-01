using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;


namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "ResetBurnOutMoveDuration", story: "reset [Mover] move duration", category: "Action", id: "020d58b8eb8b9bf846d5c03a9ebfb39d")]
    public partial class ResetBurnOutMoveDurationAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutBossMovement> Mover;

        protected override Status OnStart()
        {
            Mover.Value.ResetAxisDuration();  
            return Status.Success;
        }
    }


}