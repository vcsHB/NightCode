using Core.Attribute;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/MapInfo")]
    public class MapSO : ScriptableObject
    {
        [Header("end node is excepted")]
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
        public List<BranchInfo> branchInfo;
        public List<NodeInfo> nodeInfo;
        public List<Vector2Int> difficulty;
        public bool fixedNode;
        [Condition(nameof(fixedNode), true)] public NodeType fixedNodeType;
    }

    [Serializable]
    public struct BranchInfo
    {
        public BranchType branchType;
        [Condition(nameof(branchType), BranchType.Devide)] public int devideCount;
        [Tooltip("Will be devide by 10")]
        public int ratio;
    }

    [Serializable]
    public struct NodeInfo
    {
        public NodeType nodeType;
        public int nodeRatio;
    }

    public enum BranchType
    {
        Keep,
        Merge,
        Devide
    }
}
