using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "FlipToTarget", story: "[Self] flip to [Target] with [Threshold]", category: "Action", id: "8a4c42797dc9905b3ebd12255f60ab41")]
    public partial class FlipToTargetAction : Action
    {

        [SerializeReference] public BlackboardVariable<Enemy> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<float> Threshold;

        private AgentRenderer _renderer;

        protected override Status OnStart()
        {
            _renderer = Self.Value.GetCompo<AgentRenderer>();
            Vector3 targetPos = Target.Value.position;
            Vector3 myPos = Self.Value.transform.position;
            Vector3 direction = targetPos - myPos;

            if (Mathf.Abs(direction.x) > Threshold.Value)
                _renderer.FlipController(Mathf.Sign(direction.x));
            return Status.Success;
        }
    }

}
