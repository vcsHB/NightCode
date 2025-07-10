using Core;
using Core.DataControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksPanel : MonoBehaviour
{
    public void ReturnTitle()
    {
        DataLoader.Instance.ResetData();
        SceneManager.LoadScene(SceneName.TitleScene);
    }
}
