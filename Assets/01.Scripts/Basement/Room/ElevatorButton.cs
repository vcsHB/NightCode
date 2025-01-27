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
        Vector2 uiPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f));
        _basementPlayer.RemoveInteractAction(OnInteract);

        uiPanel.Open(uiPosition);
    }

    public override void OnInteractObject()
    {
        _basementPlayer.SetInteractAction(OnInteract);
    }

    public void Init(BasementPlayer player, ElevatorDoor door, int floor)
    {
        _basementPlayer = player;
        _floor = floor;
        _door = door;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteractObject();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _basementPlayer.RemoveInteractAction(OnInteract);
    }
}
