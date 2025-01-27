using UnityEngine;

public class FloorSelectUI : MonoBehaviour, IUIPanel
{
    public RectTransform RectTrm => transform as RectTransform;

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
