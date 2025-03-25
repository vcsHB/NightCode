#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog
{
    public class DialogView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DialogView, GraphView.UxmlTraits> { }
        public new class UxmlTraits : GraphView.UxmlTraits { }

        public Action<NodeView> OnNodeSelected;
        private DialogSO _dialog;

        public DialogSO Dialog => _dialog;

        public DialogView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public void PopulateTree(DialogSO dialog)
        {
            _dialog = dialog;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);  //자기가 가지고 있는 모든 엘리멘트 삭제
            graphViewChanged += OnGraphViewChanged;


            _dialog.nodes.ForEach(node =>
            {
                CreateNodeView(node);
                if (node is OptionNodeSO option)
                {
                    //여기서 Option에 다시 그리는걸 구독해줘
                    option.OnOptionChange = Refresh;
                }
                if (node is BranchNodeSO branch)
                {
                    branch.onChangeCondition = Refresh;
                }
            });

            //자식한테 엣지 연결해주기
            _dialog.nodes.ForEach(node =>
            {
                NodeView parent = FindNodeView(node);
                var children = _dialog.GetChildren(node);
                int index = 0;

                children.ForEach(cn =>
                {
                    if (cn != null)
                    {
                        NodeView childNV = FindNodeView(cn);

                        if (parent.output == null)
                        {
                            Debug.Log(index);
                            Edge edge = parent.outputs[index].ConnectTo(childNV.input);
                            AddElement(edge);
                        }
                        else
                        {
                            Edge edge = parent.output.ConnectTo(childNV.input);
                            AddElement(edge);
                        }
                    }
                    index++;
                });
            });
        }

        private void Refresh() => PopulateTree(_dialog);

        private NodeView FindNodeView(NodeSO node) => GetNodeByGuid(node.guid) as NodeView;

        private GraphViewChange OnGraphViewChanged(GraphViewChange changeInfo)
        {
            if (changeInfo.elementsToRemove != null)
            {
                changeInfo.elementsToRemove.ForEach(elem =>
                {
                    if (elem is NodeView nv)
                    {
                        _dialog.DeleteNode(nv.nodeSO);
                    }

                    if (elem is Edge edge)
                    {
                        int index = 0;
                        NodeView parent = edge.output.node as NodeView;
                        NodeView child = edge.input.node as NodeView;

                        if (parent.nodeSO is OptionNodeSO || parent.nodeSO is BranchNodeSO)
                        {
                            for (int i = 0; i < parent.outputs.Count; i++)
                            {
                                if (parent.outputs[i] == edge.output)
                                {
                                    index = i;
                                }
                            }
                        }

                        _dialog.RemoveChild(parent.nodeSO, child.nodeSO, index);
                    }
                });
            }

            if (changeInfo.edgesToCreate != null)
            {
                changeInfo.edgesToCreate.ForEach(elem =>
                {
                    int index = 0;
                    NodeView parent = elem.output.node as NodeView;
                    NodeView child = elem.input.node as NodeView;

                    if (parent.nodeSO is OptionNodeSO || parent.nodeSO is BranchNodeSO)
                    {
                        for (int i = 0; i < parent.outputs.Count; i++)
                        {
                            if (parent.outputs[i] == elem.output)
                            {
                                index = i;
                            }
                        }
                    }

                    _dialog.AddChild(parent.nodeSO, child.nodeSO, index);
                });
            }

            return changeInfo;
        }

        private void CreateNodeView(NodeSO node)
        {
            NodeView nv = new NodeView(node);
            nv.OnNodeSelected = OnNodeSelected;
            AddElement(nv);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_dialog == null)
            {
                evt.StopPropagation();
                return;
            }

            var types = TypeCache.GetTypesDerivedFrom<NodeSO>();
            Vector2 mousePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);

            foreach (Type type in types)
            {
                evt.menu.AppendAction(type.Name,
                    (a) => CreateNode(type, mousePosition));
            }
        }

        private void CreateNode(Type type, Vector2 position)
        {
            NodeSO node = _dialog.CreateNode(type);
            node.position = position;
            CreateNodeView(node);
        }
    }

}

#endif