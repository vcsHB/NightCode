using Agents.Enemies.Bat;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.Bat.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "BatLandCeiling", story: "land ceiling with [Mover]", category: "Action", id: "be3b062fda16e8a32f987126924fc1c2")]
    public partial class BatLandCeilingAction : Action
    {
        [SerializeReference] public BlackboardVariable<BatEnemyMovement> Mover;

        protected override Status OnStart()
        {
            Mover.Value.HandleMoveToCeiling();
            return Status.Running;
        }

        private void HandleArrived()
        {

        }

    }


}