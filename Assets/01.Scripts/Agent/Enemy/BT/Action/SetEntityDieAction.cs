using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetEntityDie", story: "set [Target] to die state", category: "Action", id: "dd44451dcfc5195ee20f02824e52278c")]
    public partial class SetEntityDieAction : Action
    {
        [SerializeReference] public BlackboardVariable<Collider2D> Target;

        protected override Status OnStart()
        {

            return Status.Success;
        }

    }


}