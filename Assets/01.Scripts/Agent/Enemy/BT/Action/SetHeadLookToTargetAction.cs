using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetHeadLookToTargetAction", story: "[HeadTrm] look to [Target] for [Duration]", category: "Action", id: "86ace7118e8ddb785b75a3403e18729a")]
    public partial class SetHeadLookToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> HeadTrm;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<BurnOutBossAttackController> AttackController;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private float _rotationTimer;

        private float _startAngle;
        private float _targetAngle;

        protected override Status OnStart()
        {
            _rotationTimer = 0f;
            _startAngle = HeadTrm.Value.localEulerAngles.z;

            // 방향 벡터 계산
            Vector2 dir = Target.Value.position - HeadTrm.Value.position;
            _targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            _rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_rotationTimer / Duration);

            // 각도 보간
            float newAngle = Mathf.LerpAngle(_startAngle, _targetAngle, t);
            HeadTrm.Value.localRotation = Quaternion.Euler(0, 0, newAngle);

            if (t < 1f)
                return Status.Running;
            return Status.Success;
        }
    }
}