using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public class TutorialGoal : MonoBehaviour
    {
        [SerializeField] private string _exitSceneName;
        [SerializeField] private UIPanel _sceneFadeUI;
        [SerializeField] private float _sceneExitDuration;

        public void HandleMoveToLobby()
        {
            StartCoroutine(MoveToLobbyCoroutine()); 
            _sceneFadeUI.Open();
            Time.timeScale = 1f;
        }

        private IEnumerator MoveToLobbyCoroutine()
        {
            yield return new WaitForSeconds(_sceneExitDuration);
            SceneManager.LoadScene(_exitSceneName);
        }

    }


}