using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "MoveToTarget", story: "move to [Target] with [Mover]", category: "Action", id: "4328d2e9cab2f6bf1baaced0fb917a5d")]
    public partial class MoveToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;

        protected override Status OnStart()
        {
            if (Target.Value == null)
            {
                return Status.Failure;
            }

            Vector3 direction = Target.Value.position - Mover.Value.transform.position;
            float movementX = Mathf.Sign(direction.x);
            Mover.Value.SetMovement(movementX);
            return Status.Success;
        }
    }

}