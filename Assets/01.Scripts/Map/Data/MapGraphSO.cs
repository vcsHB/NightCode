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

        [Space]
        public List<MapSO> maps = new();

        [Space]
        public List<CombatNodeSO> combatNodes = new();
        public List<EncounterNodeSO> encounterNodes = new();
        public List<ShopNodeSO> shopNodes = new();


        private List<MapNodeSO>[] _nodeMap;


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

            List<int>[] linkedMap = GenerateNode(selectedMap);
            ConnectNode(linkedMap);

            return _nodeMap;
        }

        private List<int>[] GenerateNode(MapSO selectedMap)
        {
            List<int>[] branchMap = new List<int>[selectedMap.depth];
            _nodeMap = new List<MapNodeSO>[selectedMap.depth + 1];

            _nodeMap[0] = new List<MapNodeSO>(1) { startNode.Instantiate() };
            _nodeMap[^1] = new List<MapNodeSO>(1) { bossNode.Instantiate() };
            _nodeMap[0][0].nextNodes = new List<MapNodeSO>();
            _nodeMap[^1][0].nextNodes = new List<MapNodeSO>();

            for (int i = 0; i < selectedMap.depth; i++)
            {
                LevelInfo currentLevel = selectedMap.levelInfo[i];
                List<MapNodeSO> mapNodes = new List<MapNodeSO>();
                List<NodeInfo> nodeInfoList = new List<NodeInfo>();
                List<int> linkedCount = new List<int>();

                int currentNodeCount = _nodeMap[i].Count;
                int nextNodeCount = currentNodeCount;

                int totalRatio = 0;
                currentLevel.nodeInfo.ForEach(info => totalRatio += info.ratio);
                int repeat = currentNodeCount / totalRatio;

                for (int j = 0; j < repeat; j++)
                    currentLevel.nodeInfo.ForEach(info => nodeInfoList.Add(info));

                List<NodeInfo> temp = currentLevel.nodeInfo.ToList();
                RandomUtility.Shuffle(temp);

                for (int j = 0; j < currentNodeCount % totalRatio; j++)
                    nodeInfoList.Add(temp[j]);

                RandomUtility.Shuffle(nodeInfoList);
                nodeInfoList.ForEach(info =>
                {
                    if (info.branchType == BranchType.Keep)
                    {
                        linkedCount.Add(1);
                    }
                    else if (info.branchType == BranchType.Divide)
                    {
                        linkedCount.Add(info.divideCount);
                        nextNodeCount += (info.divideCount - 1);
                    }
                    else if (info.branchType == BranchType.Merge)
                    {
                        linkedCount.Add(-1);
                        --nextNodeCount;
                    }
                });

                branchMap[i] = linkedCount;
                if (i == selectedMap.depth - 1) continue;

                for (int j = 0; j < nextNodeCount; j++)
                {
                    LevelInfo nextLevel = selectedMap.levelInfo[i + 1];
                    NodeType randomNode = RandomUtility.GetRandomInList(nextLevel.existNodeTypes);
                    mapNodes.Add(GetRandomNode(randomNode, nextLevel.difficultyRange));
                    mapNodes[^1].nextNodes = new List<MapNodeSO>();
                }

                _nodeMap[i + 1] = mapNodes;
            }

            return branchMap;
        }

        private void ConnectNode(List<int>[] linkMap)
        {
            for (int i = 1; i < _nodeMap.Length; i++)
            {
                int process = 0;
                for (int j = 0; j < _nodeMap[i - 1].Count; j++)
                {
                    int linkedCount = linkMap[i - 1][j];

                    if (linkedCount == 1) // Keep
                    {
                        _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process++]);
                        process = Mathf.Clamp(process, 0, _nodeMap[i].Count - 1);
                    }
                    else if (linkedCount > 1) // Divide
                    {
                        for (int k = 0; k < linkedCount; k++)
                        {
                            _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process++]);
                            process = Mathf.Clamp(process, 0, _nodeMap[i].Count - 1);
                        }
                    }
                    else if (linkedCount < 0) // Merge
                    {
                        // 병합은 여러 노드가 하나로 모이는 구조이므로, process가 범위를 벗어나지 않게 체크
                        if (process == 0)
                        {
                            _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process]);
                        }
                        else if (process >= _nodeMap[i].Count)
                        {
                            _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process - 1]);
                        }
                        else
                        {
                            int isLinkedUp = Random.Range(0, 2);
                            int idx = isLinkedUp == 0 ? process : process - 1;
                            idx = Mathf.Clamp(idx, 0, _nodeMap[i - 1].Count);
                            _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][idx]);
                        }
                    }
                }
            }
        }
    }
}
