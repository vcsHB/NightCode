using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Flip", story: "flip [Renderer]", category: "Action", id: "10376e130db5d3d4a654fc69e6041206")]
    public partial class FlipAction : Action
    {
        [SerializeReference] public BlackboardVariable<AgentRenderer> Renderer;

        protected override Status OnStart()
        {
            Renderer.Value.Flip();
            return Status.Success;
        }
    }

}
