using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/Condition/Visit")]
    public class VisitCondition : ConditionSO
    {
        [Header("이 노드를 visit만큼 방문했는가?")]
        public NodeSO node;
        [Header("방문 횟수가 더 적으면 false, 많으면 true")]
        public int visitCnt;

        public override bool Decision()
        {
            return DialogConditionManager.instance.GetVisit(node.guid) >= visitCnt;
        }

    }
}
