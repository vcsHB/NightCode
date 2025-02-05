using UnityEngine;

public class BasementBuildUI : MonoBehaviour, IUIPanel
{
    [SerializeField] private GameObject _buildUIObj;

    public void Open(Vector2 position)
    {
        _buildUIObj.SetActive(true);
    }

    public void Close()
    {
        _buildUIObj.SetActive(false);
    }
}
