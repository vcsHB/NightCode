using Agents.Animate;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "WaitForAnimation", story: "wait for [AnimationTrigger]", category: "Action", id: "7b6d451364a7a95c4859517c060af9b8")]
    public partial class WaitForAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<AnimationTrigger> AnimationTrigger;

        private bool _isTriggered;
        protected override Status OnStart()
        {
            _isTriggered = false;
            AnimationTrigger.Value.OnAnimationEnd += HandleAnimationEnd;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return _isTriggered ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
            AnimationTrigger.Value.OnAnimationEnd -= HandleAnimationEnd;
        }

        private void HandleAnimationEnd()
        {
            _isTriggered = true;
        }
    }


}