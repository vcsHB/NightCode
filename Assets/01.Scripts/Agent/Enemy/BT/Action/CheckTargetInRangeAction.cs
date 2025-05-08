using Agents.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "CheckTargetInRange", story: "[Enemy] check [Target] in [Radius]", category: "Action", id: "950280469c88e7ff892e114de36de829")]
    public partial class CheckTargetInRangeAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Enemy;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<float> Radius;

        protected override Status OnStart()
        {
            Target.Value = Enemy.Value.GetTargetInRadius(Radius.Value);
            return Status.Success;
        }
    }


}