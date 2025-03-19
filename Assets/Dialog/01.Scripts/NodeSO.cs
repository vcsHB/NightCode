using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public abstract class NodeSO : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        internal bool isFirstNode;

        public abstract List<TagAnimation> GetAllAnimations();
    }
}
