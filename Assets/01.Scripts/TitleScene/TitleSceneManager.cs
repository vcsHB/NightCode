using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{

    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField] private string _startConnectSceneName;
        
        public void HandleStart()
        {
            SceneManager.LoadScene(_startConnectSceneName);
        }

        public void HandleQuit()
        {
            Application.Quit();
        }
    }

}