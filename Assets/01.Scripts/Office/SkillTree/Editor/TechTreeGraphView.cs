using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Office.CharacterSkillTree
{
    [UxmlElement]
    public partial class TechTreeGraphView : GraphView
    {
        public Action<NodeView> OnNodeSelected;
        //public new class UxmlFactory : UxmlFactory<TechTreeGraphView, GraphView.UxmlTraits> { }

        private SkillTreeSO _tree;

        public TechTreeGraphView()
        {
            Insert(0, new GridBackground());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/Office/SkillTree/Editor/TechTreeGenerator.uss");
            styleSheets.Add(styleSheet);

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private NodeView FindNodeView(NodeSO node) => GetNodeByGuid(node.guid) as NodeView;

        public void ParpurateView(SkillTreeSO tree)
        {
            _tree = tree;
            CreateNodeAndEdge();
        }

        private void CreateNodeAndEdge()
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            bool startNodeExsist = false;
            _tree.nodes.ForEach(node =>
            {
                if (node is StartNodeSO) 
                    startNodeExsist = true;
            });

            if (startNodeExsist == false)
            {
                StartNodeSO node = _tree.CreateNode(typeof(StartNodeSO)) as StartNodeSO;
                node.id = 0;
                node.nodeName = "StartNode";
                node.guid = GUID.Generate().ToString();
                node.nextNodes = new List<NodeSO>();

                _tree.nodes.Add(node);

                //if (_tree.nodes.Count == 0) _tree.nodes.Add(node);
                //if (_tree.nodes[0] == null) _tree.nodes[0] = node;
            }

            //Create Node View
            _tree.nodes.ForEach(n => CreateNodeView(n));

            //Create Edge
            _tree.nodes.ForEach(n =>
            {
                var children = _tree.GetConnectedScripts(n);

                children.ForEach(c =>
                {
                    if (c != null)
                    {
                        NodeView parentView = FindNodeView(n);
                        NodeView childView = FindNodeView(c);

                        if (parentView.output != null)
                        {
                            Edge edge = parentView.output.ConnectTo(childView.input);
                            AddElement(edge);
                        }
                    }
                });
            });
        }

        private void CreateNode(Type type)
        {
            NodeSO node = _tree.CreateNode(type);
            if (node == null) return;
            CreateNodeView(node);
        }

        private void CreateNodeView(NodeSO n)
        {
            NodeView nodeView = new NodeView(n);

            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    NodeView nodeView = elem as NodeView;
                    if (nodeView != null)
                        _tree.DeleteScript(nodeView.node);

                    Edge edge = elem as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;

                        _tree.RemoveNextNode(parentView.node, childView.node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _tree.AddNextNode(parentView.node, childView.node);
                });
            }

            return graphViewChange;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<NodeSO>();
                foreach (var t in types)
                {
                    if (t == typeof(StartNodeSO)) continue;
                    evt.menu.AppendAction($"[{t.BaseType.Name}] {t.Name}", (a) => CreateNode(t));
                }
                //NodeSO n = ScriptableObject.CreateInstance<NodeSO>();
                //Type type = n.GetType();
            }
        }
    }
}
