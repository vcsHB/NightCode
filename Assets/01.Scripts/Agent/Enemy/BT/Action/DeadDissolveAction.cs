using Agents;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes 
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "DeadDissolve", story: "Set dissolve for [Duration] with [Renderer]", category: "Action", id: "c632717576978db8e2b7dde503ad9adb")]
    public partial class DeadDissolveAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> Duration;
        [SerializeReference] public BlackboardVariable<AgentRenderer> Renderer;

        protected override Status OnStart()
        {
            EnemyRenderer enemyRenderer = Renderer.Value as EnemyRenderer;
            enemyRenderer.dissolveDuration = Duration.Value;
            enemyRenderer.Dissolve();

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}
