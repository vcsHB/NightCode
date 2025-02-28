using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Agents.Animate;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetVariable", story: "get variable from [Self]", category: "Action", id: "8d1774d59ab41a0e67ab2a39f30cbdce")]
    public partial class GetVariableAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Entity;

        protected override Status OnStart()
        {
            Enemy enemy = Entity.Value;

            Debug.Log($"enemy.GetCompo<HighbinderMovement>()은 널인가? = {enemy.GetCompo<AgentMovement>() == null}");
            enemy.SetVariable("Mover", enemy.GetCompo<AgentMovement>());
            enemy.SetVariable("Renderer", enemy.GetCompo<AgentRenderer>());
            enemy.SetVariable("AnimTrigger", enemy.GetCompo<EnemyAnimationTrigger>());

            return Status.Running;
        }
    }


}