using Agents;
using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "DestroySequenceZero", story: "Destroy [Target] with [Mover] and [AttackController] duration [Duration]", category: "Action", id: "66d6e4fbe4bcb74899f0769b9954d3da")]
    public partial class DestroySequenceZeroAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;
        [SerializeReference] public BlackboardVariable<BurnOutBossAttackController> AttackController;
        [SerializeReference] public BlackboardVariable<float> Duration;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }


}