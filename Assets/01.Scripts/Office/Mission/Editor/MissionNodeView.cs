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
    public StageSO stage;
    public Port input;
    public Port output;
    public TextField nameInput;
    public List<Port> outputs = new List<Port>();
    public List<Button> buttons = new List<Button>();

    public MissionNodeView(StageSO stage)
    {
        this.stage = stage;
        stage.onValueChange += OnUpdateValue;

        OnUpdateValue();
        viewDataKey = stage.guid;

        style.left = stage.position.x;
        style.top = stage.position.y;

        CreateInputPorts();
        CreateOutputPorts();

        this.Add(nameInput);
    }

    ~MissionNodeView() { stage.onValueChange -= OnUpdateValue; }

    private void OnUpdateValue()
    {
        title = $"{stage.id}.{stage.displayStageName}";
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
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

        if (output != null)
        {
            output.portName = "From";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        stage.position.x = newPos.xMin;
        stage.position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }
}
