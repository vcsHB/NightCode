using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Office;
using Core.StageController;

public class MissionNodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<MissionNodeView> OnNodeSelected;
    public StageSO mission;
    public Port input;
    public Port output;
    public TextField nameInput;
    public List<Port> outputs = new List<Port>();
    public List<Button> buttons = new List<Button>();

    public MissionNodeView(StageSO mission)
    {
        this.mission = mission;

        title = mission.name;
        viewDataKey = mission.guid;

        style.left = mission.position.x;
        style.top = mission.position.y;

        CreateInputPorts();
        CreateOutputPorts();

        this.Add(nameInput);
    }


    private void CreateInputPorts()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

        if (input != null)
        {
            input.portName = "To";
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        if (output != null)
        {
            output.portName = "From";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        mission.position.x = newPos.xMin;
        mission.position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }
}
