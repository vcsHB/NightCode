using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using StatSystem;
using UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Office.CharacterSkillTree
{
    public class SkillTree : MonoBehaviour, IWindowPanel
    {
        public SkillTreeSO treeSO;
        public Dictionary<NodeSO, Node> nodeDic;
        [SerializeField] private Node nodePf;
        [SerializeField] private CoinIndicator _coinIndicator;

        public Transform nodeParent;
        public Transform edgeParent;
        public Transform edgeFillParent;
        //public UnityEvent<int, int> selectNodeEvent;

#if UNITY_EDITOR
        [SerializeField] private Vector2 _nodeOriginPos;
        [SerializeField] private DirectionEnum _nodeDirection;

        [ContextMenu("CreateNodes")]
        private void CreateNodes()
        {
            var children = nodeParent.GetComponentsInChildren<Node>().ToList();

            foreach (var child in children)
            {
                DestroyImmediate(child.gameObject);
            }

            Queue<(Vector2 position, NodeSO nodeSO)> nodeQueue = new Queue<(Vector2 position, NodeSO nodeSO)>();
            nodeQueue.Enqueue((_nodeOriginPos, treeSO.nodes[0]));

            while (nodeQueue.TryDequeue(out var node))
            {
                Vector2 nodeSize = nodePf.RectTrm.sizeDelta;

                Node nodeInstance = PrefabUtility.InstantiatePrefab(nodePf) as Node;
                nodeInstance.transform.SetParent(nodeParent);

                nodeInstance.RectTrm.anchoredPosition = node.position;
                nodeInstance.SetNode(node.nodeSO);

                int nodeCnt = node.nodeSO.nextNodes.Count;

                for (int i = 0; i < nodeCnt; i++)
                {
                    Vector2 nextPos = node.position;

                    if ((int)_nodeDirection < 2)
                    {
                        nextPos.y += (_nodeDirection == DirectionEnum.Up ? 2 : -2) * nodeSize.y;
                        nextPos.x = (node.position.x - (nodeCnt - 1) * nodeSize.x) + (2 * i * nodeSize.x);
                    }
                    else
                    {
                        nextPos.x += (_nodeDirection == DirectionEnum.Up ? 2 : -2) * nodeSize.y;
                        nextPos.y = (node.position.y - (nodeCnt - 1) * nodeSize.y) + (2 * i * nodeSize.y);
                    }

                    var posAndNode = (nextPos, node.nodeSO.nextNodes[i]);
                    nodeQueue.Enqueue(posAndNode);
                }
            }
        }

#endif

        private void Start()
        {
            Load();
        }

        public void Init()
        {
            nodeDic = new Dictionary<NodeSO, Node>();
            int childCnt = nodeParent.childCount;

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                for (int j = 0; j < childCnt; j++)
                {
                    if (nodeParent.GetChild(j).TryGetComponent(out Node node))
                    {
                        if (treeSO.nodes[i] != node.NodeType) continue;
                        nodeDic.Add(treeSO.nodes[i], node);
                        node.onPointerEnter += _coinIndicator.SetIndicator;
                        node.onPointerExit += _coinIndicator.Close;
                    }
                }
            }

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                NodeSO nodeSO = treeSO.nodes[i];

                if (nodeDic.TryGetValue(nodeSO, out Node node))
                {
                    if (nodeSO is StartNodeSO )
                        node.EnableNode(true);
                }
            }

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                NodeSO nodeSO = treeSO.nodes[i];

                if (nodeDic.TryGetValue(nodeSO, out Node node))
                    node.SetEdge();
            }
        }

        private void CalculateCoin()
        {
           
        }

        public bool TryGetNode(NodeSO nodeSO, out Node node)
        {
            if (nodeSO == null)
            {
                node = null;
                return false;
            }

            return nodeDic.TryGetValue(nodeSO, out node);
        }


        public Node GetNode(int id)
        {
            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                if (treeSO.nodes[i].id == id)
                {
                    NodeSO nodeSO = treeSO.nodes[i];
                    return nodeDic[nodeSO];
                }
            }
            return null;
        }


        #region TreeSave&Load

        public TechTreeSave GetTreeSave()
        {
            TechTreeSave treeSave = new TechTreeSave();
            treeSave.openListGUID = new List<string>();

            treeSO.nodes.ForEach(node =>
            {
                if (nodeDic[node].IsNodeEnable)
                {
                    treeSave.openListGUID.Add(node.guid);
                }
            });

            return treeSave;
        }

        public void Load()
        {
            TechTreeSave treeSave = SaveManager.Instance.GetStatValue();
            treeSave.openListGUID.ForEach(openGUI =>
            {
                NodeSO node = treeSO.nodes.Find(node => node.guid == openGUI);
                if (node != null) nodeDic[node].EnableNode(true);
            });
        }


        #endregion


        #region UI

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }


    public enum DirectionEnum
    {
        Up,
        Down,
        Left,
        Right
    }
}
