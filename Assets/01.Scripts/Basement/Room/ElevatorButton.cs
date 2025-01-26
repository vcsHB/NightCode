using Basement;
using Basement.Player;
using UnityEngine;

public class ElevatorButton : BasementObject
{
    private BasementPlayer _basementPlayer;
    private ElevatorDoor _door;
    private int _floor;

    private void OnInteract()
    {
        FloorSelectUI uiPanel = UIManager.Instance.GetUIPanel(UIType.FloorSelectPanel) as FloorSelectUI;
        Vector2 uiPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5));
        uiPanel.door = _door;

        uiPanel.Open(uiPosition);
    }

    public override void OnInteractObject()
    {
        _basementPlayer.SetInteractAction(OnInteract);
    }

    public override void OnDeInteractObject()
    {
        _basementPlayer.RemoveInteractAction();
    }

    public void Init(BasementPlayer player, ElevatorDoor door, int floor)
    {
        _basementPlayer = player;
        _floor = floor;
        _door = door;
    }
}
