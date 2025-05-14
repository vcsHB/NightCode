using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Agents.Enemies;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "CheckTarget", story: "[Enemy] check [Target] in [Radius]", category: "Action", id: "e50b4c0eb5f86186b5e4ba145a69cedc")]
    public partial class CheckTargetAction : Action
    {

        [SerializeReference] public BlackboardVariable<Enemy> Enemy;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<float> Radius;

        protected override Status OnStart()
        {
            Target.Value = Enemy.Value.GetTargetInRadius(Radius.Value);
            return Target.Value != null ? Status.Failure : Status.Success;
        }

    }


}