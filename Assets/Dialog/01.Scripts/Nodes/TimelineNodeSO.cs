using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Dialog
{
    public class TimelineNodeSO : NodeSO
    {
        public PlayableAsset playable;
        public NodeSO nextNode;

        public override List<TagAnimation> GetAllAnimations()
        {
            return null;
        }
    }
}
