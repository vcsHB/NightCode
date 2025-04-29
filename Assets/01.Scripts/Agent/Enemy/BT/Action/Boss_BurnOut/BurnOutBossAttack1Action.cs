using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "BurnOutBossAttack1", story: "attack [Target] with [AttackController] and [Mover] for [Duration]", category: "Action", id: "b48264d60ad2f8c6eda5363fdd4f0b6c")]
    public partial class BurnOutBossAttack1Action : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<BurnOutBossAttackController> AttackController;
        [SerializeReference] public BlackboardVariable<BurnOutBossMovement> Mover;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private bool _isArrived;

        protected override Status OnStart()
        {
            _isArrived = false;
            Vector2 reverseWallDirection = -AttackController.Value.GetWallDirection();
            Debug.Log(reverseWallDirection);
            AttackController.Value.SetLaserActive(true);
            Mover.Value.SetAxisDuration(30);
            Mover.Value.SetMovement(Mover.Value.BossPosition + reverseWallDirection.normalized * 51f, HandleArriveEvent);
            return Status.Running;

        }

        private void HandleArriveEvent()
        {
            AttackController.Value.SetLaserActive(false);

            _isArrived = true;
        }

        protected override Status OnUpdate()
        {
            return _isArrived ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
            Mover.Value.ResetAxisDuration();

        }

    }
}