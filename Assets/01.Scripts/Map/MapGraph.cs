using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Map
{
    public class MapGraph : MonoBehaviour
    {
        public event Action<MapNodeSO> OnClickNodeEvent;
        public event Action<MapNode> OnPointerUpNodeEvent;
        public MapGraphSO mapInfo;
        public MapNode nodePrefab;

        [SerializeField] private List<HeightInfo> _heights;
        [SerializeField] private float _xOffset;
        [SerializeField] private Transform _nodeParent;
        [SerializeField] private Transform _lineParent;

        private List<MapNodeSO>[] map;
        private Dictionary<MapNodeSO, MapNode> nodeDic;

        public void Init()
        {
            nodeDic = new Dictionary<MapNodeSO, MapNode>();
            map = mapInfo.GenerateMap();

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    MapNode node = Instantiate(nodePrefab, _nodeParent);
                    node.RectTrm.anchoredPosition = new Vector2(i * _xOffset, _heights[map[i].Count - 1].positions[j]);
                    node.onSelectNode += HandleSelectNode;
                    node.onPointerEnter += HandleSelectCharacterIcon;
                    node.Init(map[i][j], i, j);
                    nodeDic.Add(map[i][j], node);
                }
            }

            ConnectNode();
        }

        private void HandleSelectNode(MapNodeSO data)
        {
            OnClickNodeEvent?.Invoke(data);
        }

        private void HandleSelectCharacterIcon(MapNode node)
        {
            OnPointerUpNodeEvent?.Invoke(node);
        }

        private void ConnectNode()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    map[i][j].nextNodes.ForEach(next =>
                    {
                        nodeDic[map[i][j]].ConnectEdge(nodeDic[next], _lineParent);
                    });
                }
            }
        }

        public MapNode GetNode(Vector2Int position) => nodeDic[map[position.x][position.y]];

        public MapNode GetNode(MapNodeSO nextNode)
        {
            if(nodeDic.TryGetValue(nextNode, out MapNode node)) return node;
            return null;
        }
    }

    [Serializable]
    public struct HeightInfo
    {
        public List<float> positions;
    }
}