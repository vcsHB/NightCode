using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agents.Enemies.BT.BossManage.ConditionNodes
{

    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckCurrentPhase", story: "check [CurrentPhase] is [Number]", category: "Conditions", id: "92e393158531bab01180b96cfc87eb7e")]
    public partial class CheckCurrentPhaseCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<BossPhaseController> CurrentPhase;
        [SerializeReference] public BlackboardVariable<int> Number;

        public override bool IsTrue()
        {
            return CurrentPhase.Value.CurrentPhase == Number.Value;
        }
    }

}