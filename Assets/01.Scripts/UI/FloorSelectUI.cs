using UnityEngine;

public class FloorSelectUI : MonoBehaviour, IUIPanel
{
    public RectTransform RectTrm => transform as RectTransform;
    public ElevatorDoor door;

    public void OpenDoor()
    {
        door.OpenDoor();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open(Vector2 position)
    {
        gameObject.SetActive(true);
        RectTrm.anchoredPosition = position;
    }
}
