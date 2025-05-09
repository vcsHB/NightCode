using Core.StageController;
using Office;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


[UxmlElement]
public partial class MissionGraphView : GraphView
{
    public Action<MissionNodeView> OnNodeSelected;
    private StageSetSO _missionSet;

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

    private MissionNodeView FindNodeView(StageSO mission)
        => GetNodeByGuid(mission.guid) as MissionNodeView;


    public void ParpurateView(StageSetSO missionSet)
    {
        _missionSet = missionSet;
        CreateNodeAndEdge();
    }

    private void CreateNodeAndEdge()
    {
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        //Create Node View
        _missionSet.stageList.ForEach(n => CreateNodeView(n));

        //Create Edge
        _missionSet.stageList.ForEach(parentStage =>
        {
            StageSO childStage = _missionSet.GetConnectedMissions(parentStage);
            if (childStage == null) return;

            MissionNodeView parentView = FindNodeView(parentStage);
            MissionNodeView childView = FindNodeView(childStage);

            if (parentView.output != null)
            {
                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            }
        });
    }


    private void CreateNode(Type type, Vector2 position)
    {
        StageSO node = _missionSet.CreateMission(type);
        node.position = position;
        CreateNodeView(node);
    }

    private void CreateNodeView(StageSO mission)
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
                if (elem is MissionNodeView nodeView)
                {
                    _missionSet.DeleteScript(nodeView.stage);
                }

                if (elem is Edge edge)
                {
                    MissionNodeView parentView = edge.output.node as MissionNodeView;
                    MissionNodeView childView = edge.input.node as MissionNodeView;

                    _missionSet.RemoveNextNode(parentView.stage, childView.stage);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                MissionNodeView parentView = edge.output.node as MissionNodeView;
                MissionNodeView childView = edge.input.node as MissionNodeView;
                _missionSet.AddNextNode(parentView.stage, childView.stage);
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
        var types = TypeCache.GetTypesDerivedFrom<StageSO>();
        Vector2 mousePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);

        foreach (Type type in types)
        {
            evt.menu.AppendAction(type.Name,
                (a) => CreateNode(type, mousePosition));
        }
    }
}
