using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetBossHead", story: "[HeadTrm] to [Rotation] for [Duration]", category: "Action", id: "72b90cdd98fbffc4ac1538285f8ba317")]
public partial class ResetBossHeadAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> HeadTrm;
    [SerializeReference] public BlackboardVariable<float> Rotation;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private float _startAngle;
    private float _rotationTimer;
    protected override Status OnStart()
    {
        _startAngle = HeadTrm.Value.localEulerAngles.z;
        _rotationTimer = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _rotationTimer += Time.deltaTime;
        float t = Mathf.Clamp01(_rotationTimer / Duration);
        float newAngle = Mathf.LerpAngle(_startAngle, Rotation.Value, t);
        HeadTrm.Value.localRotation = Quaternion.Euler(0, 0, newAngle);

        if (t < 1f)
            return Status.Running;
        return Status.Success;
    }

}

