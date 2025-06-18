using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/Condition/Visit")]
    public class VisitCondition : ConditionSO
    {
        [Header("�� ��带 visit��ŭ �湮�ߴ°�?")]
        public NodeSO node;
        [Header("�湮 Ƚ���� �� ������ false, ������ true")]
        public int visitCnt;

        public override bool Decision()
        {
            return DialogConditionManager.Instance.GetVisit(node.guid) >= visitCnt;
        }

    }
}
