using Agents.Enemies.Bat;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agents.Enemies.Bat.BT.ConditionNodes
{

    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckTargetInRangeAndObstacle", story: "check [Target] in [Range] with [Mover] and [Enemy]", category: "Conditions", id: "2f73e4fbe8a7f0d2ede59dabcfcb0dd2")]
    public partial class CheckTargetInRangeAndObstacleCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<float> Range;
        [SerializeReference] public BlackboardVariable<BatEnemyMovement> Mover;
        [SerializeReference] public BlackboardVariable<BatEnemy> Enemy;

        public override bool IsTrue()
        {
            Transform targetTrm = Enemy.Value.GetTargetInRadius(Range.Value);
            
            return true;
        }
    }

}