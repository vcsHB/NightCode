using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public struct HeightInfo
    {
        public List<float> positions;
    }

    public class MapGraph : MonoBehaviour
    {
        public MapGraphSO mapInfo;
        public MapNode nodePrefab;

        [SerializeField] private List<HeightInfo> _heights;
        [SerializeField] private float _xOffset;

        private List<(int, int)> _visit;
        private List<MapNodeSO>[] map;
        private Dictionary<MapNodeSO, MapNode> nodeDic;

        [SerializeField] private Transform _nodeParent;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _visit = new List<(int, int)>();
            map = mapInfo.GenerateMap();
            nodeDic = new Dictionary<MapNodeSO, MapNode>();

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    Debug.Log($"{i} {j} {map[i][j]}");
                    MapNode node = Instantiate(nodePrefab, _nodeParent);
                    node.RectTrm.anchoredPosition = new Vector2(i * _xOffset, _heights[map[i].Count - 1].positions[j]);
                    node.Init(map[i][j]);
                    nodeDic.Add(map[i][j], node);
                }
            }
            ConnectNode();
        }

        private void ConnectNode()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    map[i][j].nextNodes.ForEach(next =>
                    {
                        Debug.Log(next);
                        nodeDic[map[i][j]].ConnectEdge(nodeDic[next]);
                    });
                }
            }
        }
    }
}