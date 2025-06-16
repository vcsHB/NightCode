using Agents.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
namespace Agents.Enemies.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "WaitForAttackEnd", story: "wait for [Attack]", category: "Action", id: "004eb905ed8fdaa30a41ccbdaf4bce63")]
    public partial class WaitForAttackEndAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyAttackController> Attack;

        private bool _isTriggered;
        protected override Status OnStart()
        {
            _isTriggered = false;
            Attack.Value.OnAttackEndEvent += HandleAnimationEnd;
            Attack.Value.Attack();
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return _isTriggered ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
            Attack.Value.OnAttackEndEvent -= HandleAnimationEnd;
        }

        private void HandleAnimationEnd()
        {
            _isTriggered = true;
        }
    }


}