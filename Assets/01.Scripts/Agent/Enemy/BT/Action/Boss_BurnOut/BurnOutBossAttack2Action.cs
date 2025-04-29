using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "BurnOutBossAttack2", story: "[HeadTrm] attack DS_2 [Target] with [AttackController] and [Mover] for [Duration]", category: "Action", id: "72664198730b5120c7c5dcff64163237")]
    public partial class BurnOutBossAttack2Action : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> HeadTrm;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<BurnOutBossAttackController> AttackController;
        [SerializeReference] public BlackboardVariable<BurnOutBossMovement> Mover;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private float _startAngle;
        private float _targetAngle;
        private float _rotationTimer;
        private bool _isRotateCompleted;
        protected override Status OnStart()
        {

            AttackController.Value.SetLaserActive(true);

            if (Target.Value != null)
            {
                Debug.Log("Setting");
                Vector2 toTarget = Target.Value.position - HeadTrm.Value.position;
                _startAngle = HeadTrm.Value.localEulerAngles.z;

                // 타겟이 왼쪽에 있을 경우 → 목표 각도 0 (시계 방향)
                // 오른쪽에 있을 경우 → 목표 각도 360 (=0) (반시계 방향)
                _targetAngle = (toTarget.x < 0) ? -180f : 180f;

                _rotationTimer = 0f;
                _isRotateCompleted = false;
            }
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            _rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_rotationTimer / Duration);
            float newAngle = Mathf.LerpAngle(_startAngle, _targetAngle, t);
            HeadTrm.Value.localRotation = Quaternion.Euler(0, 0, newAngle);
            Debug.Log("newAngle" + newAngle);

            if (t < 1f)
                return Status.Running;
            _isRotateCompleted = true;
            AttackController.Value.SetLaserActive(false);
            return Status.Success;
        }

    }


}