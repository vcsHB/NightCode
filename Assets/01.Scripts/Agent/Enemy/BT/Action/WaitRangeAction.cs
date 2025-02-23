using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BT
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait (Range)", story: "Wait between [Min] and [Max]", category: "Action", id: "235aec37d05108d43123bf9f65deff8c")]
    public partial class WaitRangeAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> Min = new BlackboardVariable<float>(1);
        [SerializeReference] public BlackboardVariable<float> Max = new BlackboardVariable<float>(3);
        [CreateProperty] private float m_Timer = 0.0f;

        protected override Status OnStart()
        {
            m_Timer = UnityEngine.Random.Range(Min, Max);
            if (m_Timer <= 0.0f)
            {
                return Status.Success;
            }

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                return Status.Success;
            }

            return Status.Running;
        }
    }
}

