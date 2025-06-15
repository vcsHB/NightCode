using Agents.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "InvokeEnemyDetectTargetEvent", story: "Invoke detect target event with [EventTrigger]", category: "Action", id: "4a9637b4305f072ab22df3aa204cb708")]
    public partial class InvokeEnemyDetectTargetEventAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyAnimationTrigger> EventTrigger;

        protected override Status OnStart()
        {
            EventTrigger.Value.HandleDetectTarget();
            return Status.Success;
            
        }
    }


}