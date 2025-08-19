using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Core.DataControl;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private List<int>[] branchMap;

        public MapNodeSO GetNodeSO(int nodeId)
        {
            if (startNode.nodeId == nodeId) return startNode;
            if (bossNode.nodeId == nodeId) return bossNode;

            MapNodeSO node = combatNodes.Find(node => node.nodeId == nodeId);
            if (node != null) return node;
            node = encounterNodes.Find(node => node.nodeId == nodeId);
            if (node != null) return node;
            node = shopNodes.Find(node => node.nodeId == nodeId);
            if (node != null) return node;

            return null;
        }

        public MapNodeSO GetRandomNode(NodeType nodeType, StageDifficultySO difficulty)
        {
            switch (nodeType)
            {
                case NodeType.Start:
                    return startNode;
                case NodeType.Boss:
                    return bossNode;
                case NodeType.Combat:
                    return GetRandomNodeByDifficulty(combatNodes.ConvertAll(node => node as MapNodeSO), difficulty);
                case NodeType.Encounter:
                    return GetRandomNodeByDifficulty(encounterNodes.ConvertAll(node => node as MapNodeSO), difficulty);
                case NodeType.Shop:
                    return GetRandomNodeByDifficulty(shopNodes.ConvertAll(node => node as MapNodeSO), difficulty);
            }

            return null;
        }

        private MapNodeSO GetRandomNodeByDifficulty(List<MapNodeSO> nodes, StageDifficultySO difficulty)
        {
            var selectedNodeList = nodes.Where(node => node.difficulty == difficulty).ToList();
            return RandomUtility.GetRandomInList(selectedNodeList).Instantiate();
        }

        public MapInfo GenerateMap()
        {
            MapSO selectedMap = maps[Random.Range(0, maps.Count)];
            MapInfo mapInfo = new MapInfo();

            GenerateNode(selectedMap);
            mapInfo.map = _nodeMap;
            mapInfo.connections = ConnectNode();

            return mapInfo;
        }

        private void GenerateNode(MapSO selectedMap)
        {
            branchMap = new List<int>[selectedMap.depth];
            _nodeMap = new List<MapNodeSO>[selectedMap.depth + 1];

            _nodeMap[0] = new List<MapNodeSO>(1) { startNode.Instantiate() };
            _nodeMap[^1] = new List<MapNodeSO>(1) { bossNode.Instantiate() };
            _nodeMap[0][0].nextNodes = new List<MapNodeSO>();
            _nodeMap[^1][0].nextNodes = new List<MapNodeSO>();

            // generate each level
            for (int i = 0; i < selectedMap.depth; i++)
            {
                LevelInfo currentLevel = selectedMap.levelInfo[i];
                List<MapNodeSO> mapNodes = new List<MapNodeSO>();
                List<NodeInfo> nodeInfoList = new List<NodeInfo>();
                List<int> linkedCount = new List<int>();

                int nextNodeCount = _nodeMap[i].Count;

                //Make Node Connection by random
                for (int j = 0; j < 10; j++)
                    nodeInfoList.Add(currentLevel.nodeInfo[j % currentLevel.nodeInfo.Count]);
                RandomUtility.Shuffle(nodeInfoList);

                for (int j = 0; j < _nodeMap[i].Count; j++)
                {
                    NodeInfo info = nodeInfoList[j];
                    if (selectedMap.levelInfo[i].limitNodeCount &&
                        linkedCount.Count >= selectedMap.levelInfo[i].maxNodeCount)
                    {
                        linkedCount.Add(-1);
                        --nextNodeCount;
                    }
                    else
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
                    }
                }

                branchMap[i] = linkedCount;
                if (i == selectedMap.depth - 1) continue;

                for (int j = 0; j < nextNodeCount; j++)
                {
                    LevelInfo nextLevel = selectedMap.levelInfo[i + 1];
                    NodeType randomNode = RandomUtility.GetRandomInList(nextLevel.existNodeTypes);
                    mapNodes.Add(GetRandomNode(randomNode, nextLevel.difficulty));
                    mapNodes[^1].nextNodes = new List<MapNodeSO>();
                }

                _nodeMap[i + 1] = mapNodes;
            }
        }

        private List<List<Vector2Int>>[] ConnectNode()
        {
            List<List<Vector2Int>>[] connections = new List<List<Vector2Int>>[branchMap.Length];

            try
            {
                for (int i = 1; i < _nodeMap.Length; i++)
                {
                    int process = 0;
                    List<List<Vector2Int>> levelConnection = new List<List<Vector2Int>>();
                    for (int j = 0; j < _nodeMap[i - 1].Count; j++)
                    {
                        int linkedCount = branchMap[i - 1][j];
                        if (linkedCount == 1) // Keep
                        {
                            levelConnection.Add(new List<Vector2Int>(1) { new Vector2Int(i, process) });
                            _nodeMap[i][process].prevNodes.Add(_nodeMap[i - 1][j]);
                            _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process++]);
                            process = Mathf.Clamp(process, 0, _nodeMap[i].Count - 1);
                        }
                        else if (linkedCount > 1) // Divide
                        {
                            List<Vector2Int> connection = new List<Vector2Int>();
                            for (int k = 0; k < linkedCount; k++)
                            {
                                connection.Add(new Vector2Int(i, process));
                                _nodeMap[i][process].prevNodes.Add(_nodeMap[i - 1][j]);
                                _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process]);
                                process = Mathf.Clamp(process + 1, 0, _nodeMap[i].Count - 1);
                            }
                            levelConnection.Add(connection);
                        }
                        else if (linkedCount < 0) // Merge
                        {
                            if (process == 0)
                            {
                                levelConnection.Add(new List<Vector2Int>(1) { new Vector2Int(i, process) });
                                _nodeMap[i][process].prevNodes.Add(_nodeMap[i - 1][j]);
                                _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process]);
                            }
                            else if (process >= _nodeMap[i].Count)
                            {
                                levelConnection.Add(new List<Vector2Int>(1) { new Vector2Int(i, process - 1) });
                                _nodeMap[i][process - 1].prevNodes.Add(_nodeMap[i - 1][j]);
                                _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][process - 1]);
                            }
                            else
                            {
                                int isLinkedUp = Random.Range(0, 2);
                                int idx = isLinkedUp == 0 ? process : process - 1;
                                idx = Mathf.Clamp(idx, 0, _nodeMap[i - 1].Count);
                                levelConnection.Add(new List<Vector2Int>(1) { new Vector2Int(i, idx) });
                                _nodeMap[i][idx].prevNodes.Add(_nodeMap[i - 1][j]);
                                _nodeMap[i - 1][j].nextNodes.Add(_nodeMap[i][idx]);
                            }
                        }

                        connections[i - 1] = levelConnection;
                    }
                }
            }
            catch (Exception e)
            {
                File.Delete(DataLoader.Instance.MapPath);
                SceneManager.LoadScene(SceneName.MapSelectScene);
            }


            return connections;
        }
    }
}
