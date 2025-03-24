using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/DialogSO")]
    public class DialogSO : ScriptableObject
    {
        public List<NodeSO> nodes;


#if UNITY_EDITOR
        public NodeSO CreateNode(Type type)
        {
            NodeSO node = ScriptableObject.CreateInstance(type) as NodeSO;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(NodeSO node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(NodeSO parent, NodeSO nextNode, int index)
        {
            if (parent is NormalNodeSO node)
            {
                node.nextNode = nextNode;
                return;
            }

            if (parent is OptionNodeSO option)
            {
                option.AddOption(nextNode, index);
                return;
            }

            if (parent is BranchNodeSO branch)
            {
                branch.nextNodes[index] = nextNode;
                return;
            }
        }

        public void RemoveChild(NodeSO parent, NodeSO child, int index)
        {
            if (parent is NormalNodeSO node)
            {
                node.nextNode = null;
                return;
            }

            if (parent is OptionNodeSO option)
            {
                option.RemoveEdge(index);
                return;
            }


            if (parent is BranchNodeSO branch)
            {
                branch.nextNodes[index] = null;
                return;
            }
        }

        public List<NodeSO> GetChildren(NodeSO node)
        {
            List<NodeSO> children = new List<NodeSO>();

            if (node is NormalNodeSO normal)
                children.Add(normal.nextNode);

            if (node is OptionNodeSO option)
                option.options.ForEach(opt => children.Add(opt.nextNode));

            if (node is BranchNodeSO branch)
                children = branch.nextNodes;

            return children;
        }
#endif
    }
}
