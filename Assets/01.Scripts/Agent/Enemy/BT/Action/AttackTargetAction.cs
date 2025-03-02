using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "AttackTarget", story: "attack [Target] with [AttackController]", category: "Action", id: "4b9dfa1e8a903980dd899266c903ae9d")]
    public partial class AttackTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<EnemyAttackController> AttackController;

        protected override Status OnStart()
        {
            if (AttackController.Value.HandleAttack(Target.Value))
                return Status.Success;
            else
                return Status.Failure;
        }


    }
}
