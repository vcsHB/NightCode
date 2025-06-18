using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class BranchNodeSO : NodeSO
    {
        public ConditionSO condition;
        [HideInInspector] public List<NodeSO> nextNodes = new List<NodeSO>(2);

        public Action onChangeCondition;

        public override List<TagAnimation> GetAllAnimations()
        {
            List<TagAnimation> anims = new List<TagAnimation>();
            return anims;
        }

        private void OnEnable()
        {
            if (nextNodes.Count < 2)
            {
                nextNodes.Add(null);
                nextNodes.Add(null);
            }
        }

        private void OnValidate()
        {
            onChangeCondition?.Invoke();
        }
    }
}
