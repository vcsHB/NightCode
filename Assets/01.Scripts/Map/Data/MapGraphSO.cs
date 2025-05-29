using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/MapGraphInfo")]
    public class MapGraphSO : ScriptableObject
    {
        public StartNodeSO startNode;
        public BossNodeSO bossNode;
        //보스 노드

        [Space]
        public List<MapSO> maps = new();

        [Space]
        public List<CombatNodeSO> combatNodes = new();
        public List<EncounterNodeSO> encounterNodes = new();
        public List<ShopNodeSO> shopNodes = new();


        private List<MapNodeSO>[] _nodes;


        public MapNodeSO GetRandomNode(NodeType nodeType, Vector2Int difficultyRange)
        {
            if (difficultyRange.x > difficultyRange.y)
            {
                difficultyRange.x ^= difficultyRange.y;
                difficultyRange.y ^= difficultyRange.x;
                difficultyRange.x ^= difficultyRange.y;
            }

            switch (nodeType)
            {
                case NodeType.Start:
                    return startNode;
                case NodeType.Boss:
                    return bossNode;
                case NodeType.Combat:
                    var combats = combatNodes.Where(node =>
                    Mathf.Abs(node.difficulty - difficultyRange.x) >= 0
                    && Mathf.Abs(difficultyRange.y - node.difficulty) >= 0).ToList();
                    return RandomUtility.GetRandomInList(combats).Instantiate();
                case NodeType.Encounter:
                    var encounters = combatNodes.Where(node =>
                    Mathf.Abs(node.difficulty - difficultyRange.x) >= 0
                    && Mathf.Abs(difficultyRange.y - node.difficulty) >= 0).ToList();
                    return RandomUtility.GetRandomInList(encounters).Instantiate();
                case NodeType.Shop:
                    var shops = combatNodes.Where(node =>
                    Mathf.Abs(node.difficulty - difficultyRange.x) >= 0
                    && Mathf.Abs(difficultyRange.y - node.difficulty) >= 0).ToList();
                    return RandomUtility.GetRandomInList(shops).Instantiate();
            }

            return null;
        }

        public MapNodeSO GetRandomNode(List<NodeInfo> nodeInfos, Vector2Int difficultyRange)
        {
            NodeType randomNode = (NodeType)Random.Range(1, 4);
            return GetRandomNode(randomNode, difficultyRange);
        }

        public List<MapNodeSO>[] GenerateMap()
        {
            MapSO selectedMap = maps[Random.Range(0, maps.Count)];

            //무조건 10개로 할래요
            BranchInfo[,] branchField = new BranchInfo[selectedMap.depth, 10];

            for (int i = 0; i < selectedMap.depth; i++)
            {
                LevelInfo currentLevelInfo = selectedMap.levelInfo[i];
                List<BranchInfo> branchInfos = new List<BranchInfo>();
                currentLevelInfo.branchInfo.ForEach(branch =>
                {
                    for (int i = 0; i < branch.ratio; i++)
                    {
                        branchInfos.Add(branch);
                    }
                });
                RandomUtility.Shuffle(branchInfos);

                for (int j = 0; j < 10; j++)
                    branchField[i, j] = branchInfos[j];
            }

            int startIndex = Random.Range(0, 10);
            _nodes = new List<MapNodeSO>[selectedMap.depth + 1];
            MapNodeSO startNode = Generate(0, startIndex, branchField, selectedMap);

            for(int i = 0; i < _nodes.Length; i++)
            {
                Debug.Log(_nodes[i].Count);
            }

            return _nodes;
        }


        public MapNodeSO Generate(int depth, int height, BranchInfo[,] branchField, MapSO selectedMap)
        {
            if (depth == selectedMap.depth)
            {
                if (_nodes[depth] == null)
                {
                    _nodes[depth] = new List<MapNodeSO>();
                    _nodes[depth].Add(bossNode.Instantiate());
                }
                return _nodes[depth][0];
            }

            if (_nodes[depth] == null) _nodes[depth] = new List<MapNodeSO>();
            LevelInfo level = selectedMap.levelInfo[depth];
            MapNodeSO currentNode;

            if (depth == 0) currentNode = startNode.Instantiate();
            else
            {
                Vector2Int difficultyRange = level.difficulty[height];

                if (level.fixedNode)
                {
                    currentNode = GetRandomNode(level.fixedNodeType, difficultyRange);
                }
                else
                {
                    currentNode = GetRandomNode(level.nodeInfo, difficultyRange);
                }
            }

            int newHeight;
            List<int> values = new List<int>(3) { -1, 0, 1 };
            _nodes[depth].Add(currentNode);

            switch (branchField[depth, height].branchType)
            {
                case BranchType.Keep:
                    {
                        newHeight = height + RandomUtility.GetRandomInList<int>(values);
                        if (newHeight < 0) newHeight = 9;
                        if (newHeight >= 10) newHeight = 0;

                        currentNode.nextNodes.Add(Generate(depth + 1, newHeight, branchField, selectedMap));
                    }
                    break;
                case BranchType.Devide:
                    {
                        List<int> heightDiffrents = RandomUtility.GetRandomsInListNotDuplicated<int>(values, branchField[depth, height].devideCount);
                        for (int i = 0; i < branchField[depth, height].devideCount; i++)
                        {
                            newHeight = height + heightDiffrents[i];
                            if (newHeight < 0) newHeight = 9;
                            if (newHeight >= 10) newHeight = 0;

                            MapNodeSO map = Generate(depth + 1, newHeight, branchField, selectedMap);
                            
                            if(currentNode.nextNodes.Contains(map) == false)
                                currentNode.nextNodes.Add(map);
                        }
                    }
                    break;
                case BranchType.Merge:
                    {
                        newHeight = height + RandomUtility.GetRandomInList<int>(values);
                        if (newHeight < 0) newHeight = 9;
                        if (newHeight >= 10) newHeight = 0;

                        MapNodeSO nextNode = Generate(depth + 1, newHeight, branchField, selectedMap);
                        Debug.Log(_nodes[depth].Count);
                        if (_nodes[depth].Count > 0)
                            _nodes[depth][^1].nextNodes.Add(nextNode);

                        currentNode.nextNodes.Add(nextNode);
                    }
                    break;
            }

            Debug.Log($"{depth} | {currentNode.nextNodes.Count}");
            return currentNode;
        }
    }
}
