using Agents;
using Combat;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "WaitForStun", story: "wait for [StunBody] with [Mover]", category: "Action", id: "abd6f87ecfc92b6553ba9679986a1d65")]
    public partial class WaitForStunAction : Action
    {
        [SerializeReference] public BlackboardVariable<StunBody> StunBody;
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;

        
        
    }


}
