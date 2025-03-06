using Agents;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agents.Enemies.BT.ConditionNodes
{

    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckGround", story: "[mover] ground is [value]", category: "Conditions", id: "d8b6f1dc8653a45ad9495c007d719cae")]
    public partial class CheckGroundCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<AgentMovement> Mover;
        [SerializeReference] public BlackboardVariable<bool> Value;

        public override bool IsTrue()
        {
            bool isGround = Mover.Value.IsGroundDetected();
            return isGround == Value.Value;
        }

    }

}