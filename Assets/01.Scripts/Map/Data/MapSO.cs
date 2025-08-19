using Core.Attribute;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/MapInfo")]
    public class MapSO : ScriptableObject
    {
        [Header("start and end node is excepted")]
        public int depth;
        public List<LevelInfo> levelInfo;
        

        private void OnValidate()
        {
            if (levelInfo == null) levelInfo = new List<LevelInfo>(depth);

            if (levelInfo.Count < depth) levelInfo.Add(default);
            if (levelInfo.Count > depth) levelInfo.RemoveAt(depth);

            //levelInfo.ForEach(level => level.OnValidate());
        }
    }

    [Serializable]
    public struct LevelInfo
    {
        public List<NodeInfo> nodeInfo;
        public List<NodeType> existNodeTypes;
        public StageDifficultySO difficulty;
        public bool isFixedNode;
        public bool limitNodeCount;
        [Condition(nameof(isFixedNode), true)] public MapNodeSO fixedNode;
        [Condition(nameof(limitNodeCount), true)] public int maxNodeCount;
    }

    [Serializable]
    public struct NodeInfo
    {
        public BranchType branchType;
        public int ratio;
        [Space]
        [Condition(nameof(branchType), BranchType.Divide)]public int divideCount;
    }

    public enum BranchType
    {
        Keep,
        Merge,
        Divide
    }
}
