using DG.Tweening;
using UnityEngine;

public class RequestPanel : MonoBehaviour
{

    public void Open()
    {
        transform.DOScale(1, 0.3f).SetUpdate(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        Time.timeScale = 1;
        transform.DOScale(0, 0.3f);
    }
}
