using Agents.Enemies.Bat;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.Bat.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetBatVariable", story: "get variable from [Enemy]", category: "Action", id: "fbb234216932e7ac0ffa4da529e592cc")]
    public partial class GetBatVariableAction : Action
    {
        [SerializeReference] public BlackboardVariable<BatEnemy> Enemy;

        protected override Status OnStart()
        {
            BatEnemy enemy = Enemy.Value;
            enemy.SetVariable("Mover", enemy.GetCompo<BatEnemyMovement>());

            return Status.Running;
        }

    }
}

