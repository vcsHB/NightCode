using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class EventNodeSO : NodeSO
    {
        public NodeSO nextNode;

        public override List<TagAnimation> GetAllAnimations()
        {
            return null;
        }
    }
}
