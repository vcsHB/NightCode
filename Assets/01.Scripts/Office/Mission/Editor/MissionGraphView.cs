using Office;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections.Generic;
using System.Linq;


[UxmlElement]
public partial class MissionGraphView : GraphView
{
    public Action<MissionNodeView> OnNodeSelected;
    private MissionSetSO _missionSet;

    public MissionGraphView()
    {
        Insert(0, new GridBackground());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/Office/Mission/Editor/MissionFlowEditor.uss");
        styleSheets.Add(styleSheet);

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    }

    private MissionNodeView FindNodeView(MissionSO mission) 
        => GetNodeByGuid(mission.guid) as MissionNodeView;


    public void ParpurateView(MissionSetSO missionSet)
    {
        _missionSet = missionSet;
        CreateNodeAndEdge();
    }

    private void CreateNodeAndEdge()
    {
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (_missionSet.missionList.Count == 0 || _missionSet.missionList[0] == null)
        {
            MissionSO mission = _missionSet.CreateMission();
            mission.id = 0;
            mission.guid = GUID.Generate().ToString();
            mission.nextMissions = new List<MissionSO>();
        }

        //Create Node View
        _missionSet.missionList.ForEach(n => CreateNodeView(n));

        //Create Edge
        _missionSet.missionList.ForEach(parentMission =>
        {
            var children = _missionSet.GetConnectedMissions(parentMission);

            children.ForEach(childMission =>
            {
                if (childMission != null)
                {
                    MissionNodeView parentView = FindNodeView(parentMission);
                    MissionNodeView childView = FindNodeView(childMission);

                    if (parentView.output != null)
                    {
                        Edge edge = parentView.output.ConnectTo(childView.input);
                        AddElement(edge);
                    }
                }
            });
        });
    }


    private void CreateNode()
    {
        MissionSO node = _missionSet.CreateMission();
        if (node == null) return;
        CreateNodeView(node);
    }

    private void CreateNodeView(MissionSO mission)
    {
        MissionNodeView nodeView = new MissionNodeView(mission);

        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                MissionNodeView nodeView = elem as MissionNodeView;
                if (nodeView != null)
                    _missionSet.DeleteScript(nodeView.mission);

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    MissionNodeView parentView = edge.output.node as MissionNodeView;
                    MissionNodeView childView = edge.input.node as MissionNodeView;

                    _missionSet.RemoveNextNode(parentView.mission, childView.mission);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                MissionNodeView parentView = edge.output.node as MissionNodeView;
                MissionNodeView childView = edge.input.node as MissionNodeView;
                _missionSet.AddNextNode(parentView.mission, childView.mission);
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
            evt.menu.AppendAction($"Mission", (a) => CreateNode());
            //NodeSO n = ScriptableObject.CreateInstance<NodeSO>();
            //Type type = n.GetType();
        }
    }
}
