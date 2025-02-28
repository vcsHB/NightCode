using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;


namespace Agents.Enemies.BT.ActionNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "MoveToFront", story: "move to [Renderer] direction with [Mover]", category: "Action", id: "9f2bc0a2df4e7c8e20043c745c45b38c")]
    public partial class MoveToFrontAction : Action
    {
        [SerializeReference] public BlackboardVariable<AgentRenderer> Renderer;
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;

        protected override Status OnStart()
        {
            float xDirection = Renderer.Value.FacingDirection;
            Mover.Value.SetMovement(xDirection);
            return Status.Success;
        }
    }

}


