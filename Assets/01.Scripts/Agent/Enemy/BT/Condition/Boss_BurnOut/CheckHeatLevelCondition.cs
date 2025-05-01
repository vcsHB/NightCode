using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agents.Enemies.BT.BossManage.ConditionNodes
{

    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckHeatLevel", story: "check [Heat] is hotter than [HeatLevel]", category: "Conditions", id: "d9a3aadd9655b16d77fec4150e368913")]
    public partial class CheckHeatLevelCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<BossHeatController> Heat;
        [SerializeReference] public BlackboardVariable<int> HeatLevel;

        public override bool IsTrue()
        {
            return Heat.Value.CurrentHeatLevel > HeatLevel.Value;
        }

    }

}