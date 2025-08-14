using Core;
using Core.DataControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{

    public class TutorialManager : MonoBehaviour
    {

        public void ClearTutotial()
        {
            DataLoader.Instance.GetUserData().isClearTutorial = true;
            DataLoader.Instance.Save();
        }

        public void HandleExitTutorial()
        {
            SceneManager.LoadScene(SceneName.TitleScene);
            Time.timeScale = 1f;
        }


    }

}