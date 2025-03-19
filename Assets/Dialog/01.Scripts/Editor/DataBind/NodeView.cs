#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Dialog;

namespace Dialog
{
    public class NodeView : Node
    {
        public Action<NodeView> OnNodeSelected;
        public NodeSO nodeSO;
        public Port input;
        public Port output;
        public List<Port> inputs = new();
        public List<Port> outputs = new();

        public NodeView(NodeSO nodeSO) : base("Assets/Dialog/01.Scripts/Editor//DataBind/NodeView.uxml")        {
            this.nodeSO = nodeSO;
            this.title = nodeSO.name;
            this.viewDataKey = nodeSO.guid;

            style.top = nodeSO.position.y;
            style.left = nodeSO.position.x;

            CreateInputPort();
            CreateOutputPort();
        }

        private void CreateOutputPort()
        {
            if (nodeSO is NormalNodeSO)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (nodeSO is OptionNodeSO option)
            {
                for (int i = 0; i < option.options.Count; i++)
                {
                    Port o = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                    if (o != null)
                    {
                        o.portName = "";
                        outputs.Add(o);
                        outputContainer.Add(o);
                    }
                }
            }
            else if (nodeSO is BranchNodeSO branch)
            {
                if (branch.condition == null) return;

                Port tOutput = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                if (tOutput != null)
                {
                    tOutput.portName = "True";
                    outputs.Add(tOutput);
                    outputContainer.Add(tOutput);
                }


                Port fOutput = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                if (fOutput != null)
                {
                    fOutput.portName = "False";
                    outputs.Add(fOutput);
                    outputContainer.Add(fOutput);
                }
            }

            if (output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }

        private void CreateInputPort()
        {
            //루트노드 체크는 나중에
            if (nodeSO.isFirstNode) return;
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

            if (input != null)
            {
                input.portName = "";
                inputContainer.Add(input);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            nodeSO.position.x = newPos.xMin;
            nodeSO.position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }

}

#endif