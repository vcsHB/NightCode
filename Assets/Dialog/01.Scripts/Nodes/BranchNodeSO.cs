using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class BranchNodeSO : NodeSO
    {
        public ConditionSO condition;
        [HideInInspector] public List<NodeSO> nextNodes = new List<NodeSO>();

        public Action onChangeCondition;

        public override List<TagAnimation> GetAllAnimations()
        {
            List<TagAnimation> anims = new List<TagAnimation>();
            return anims;
        }

        private void OnValidate()
        {
            onChangeCondition?.Invoke();
        }
    }
}
