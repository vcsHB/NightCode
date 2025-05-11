using Agents.Enemies.Bat;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.Bat.BT.ActionNodes
{


    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "BatFollowTarget", story: "follow [Target] with [Mover] for [Duration]", category: "Action", id: "65792a5d660e8d132a745bbee049a12c")]
    public partial class BatFollowTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<BatEnemyMovement> Mover;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private float _startTime;
        protected override Status OnStart()
        {
            _startTime = Time.time;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_startTime + Duration.Value < Time.time)
            {
                return Status.Success;
            }
            Vector2 direction = Target.Value.position - Mover.Value.transform.position;
            Mover.Value.SetMovement(direction);
            return Status.Running;
        }

    }


}