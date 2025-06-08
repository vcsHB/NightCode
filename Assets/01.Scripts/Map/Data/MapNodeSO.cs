using Core.Attribute;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public abstract class MapNodeSO : ScriptableObject
    {
        public int nodeId;
        [ReadOnly] public NodeType nodeType;

        public string displayName;
        public string explain;
        public Sprite icon;
        public Color color;

        [Space]
        public StageDifficultySO difficulty;

        [HideInInspector] public List<MapNodeSO> nextNodes = new List<MapNodeSO>();
        //MAP

        public abstract MapNodeSO Instantiate();

        private void OnValidate()
        {
            nextNodes?.Clear();
        }
    }

    public enum NodeType
    {
        Start,
        Combat,
        Encounter,
        Shop,
        Boss,
    }
}
