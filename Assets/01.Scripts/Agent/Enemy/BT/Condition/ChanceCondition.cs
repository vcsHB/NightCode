using System;
using Unity.Behavior;
using UnityEngine;


namespace Agents.Enemies.BT.ConditionNodes
{

    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "ChanceCondition", story: "get chance in [Number] % in [Max]", category: "Conditions", id: "b7f64da8bdf4014123dc74a569afbaec")]
    public partial class ChanceCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<int> Number;
        [SerializeReference] public BlackboardVariable<int> Max;

        public override bool IsTrue()
        {
            return Number.Value > UnityEngine.Random.Range(0, Max.Value);

        }

    }

}