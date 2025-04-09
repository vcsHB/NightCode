using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Office.CharacterSkillTree
{
    public class SkillTree : OfficeUIParent
    {
        public SkillTreeSO treeSO;
        public Dictionary<NodeSO, Node> nodeDic;
        public CharacterEnum characterType;
        [SerializeField] private Node nodePf;

        public Transform edgeParent;
        public Transform edgeFillParent;
        private string _path;

        [SerializeField] private Vector2 _nodeOriginPos;
        [SerializeField] private DirectionEnum _nodeDirection;

        public UnityEvent<int, int> selectNodeEvent;

        private void Awake()
        {
            nodeDic = new Dictionary<NodeSO, Node>();
            _path = Path.Combine(Application.dataPath, "TechTree.json");
        }

        private void Start()
        {
            int childCnt = transform.childCount;

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                for (int j = 0; j < childCnt; j++)
                {
                    if (transform.GetChild(j).TryGetComponent(out Node node))
                    {
                        if (treeSO.nodes[i].id != node.NodeType.id) continue;

                        nodeDic.Add(node.NodeType, node);
                    }
                }
            }

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                NodeSO nodeSO = treeSO.nodes[i];

                if (nodeDic.TryGetValue(nodeSO, out Node node))
                {
                    if (node.NodeType.id == 0) node.EnableNode();
                    node.Init(characterType);
                }
            }

            for (int i = 0; i < treeSO.nodes.Count; i++)
            {
                NodeSO nodeSO = treeSO.nodes[i];

                if (nodeDic.TryGetValue(nodeSO, out Node node))
                    node.SetEdge();
            }

            //Load();
        }


#if UNITY_EDITOR

        [ContextMenu("CreateNodes")]
        private void CreateNodes()
        {
            var children = transform.GetComponentsInChildren<Node>().ToList();

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
                nodeInstance.transform.SetParent(transform);

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
            Debug.Log(id);
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


        public void Save()
        {
            List<Node> parts = new List<Node>();
            List<Node> weapons = new List<Node>();
            treeSO.nodes.ForEach(n =>
            {
                //if (n is PartNodeSO)
                //{
                //    if (TryGetNode(n, out Node node))
                //    {
                //        parts.Add(node);
                //    }
                //}

                //if (n is WeaponNodeSO)
                //{
                //    if (TryGetNode(n, out Node node))
                //    {
                //        weapons.Add(node);
                //    }
                //}
            });


        }

        public void Load()
        {
            treeSO.nodes.ForEach(n =>
            {

            });
        }


        public override void OpenAnimation()
        {
            gameObject.SetActive(true);
        }

        public override void CloseAnimation()
        {
            gameObject.SetActive(false);
        }
    }


    public enum DirectionEnum
    {
        Up,
        Down,
        Left,
        Right
    }


}
