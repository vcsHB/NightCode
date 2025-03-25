using System.Collections;
using Combat.PlayerTagSystem;
using QuestSystem.LevelSystem;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private LevelMap _tutorialLevel;
        [SerializeField] private UIPanel _exitPanel;
        [SerializeField] private float _sceneExitDelay;


        private void Start()
        {
            MovePlayerToStartPos();
        }

        private void Update()
        {
        }

        private void MovePlayerToStartPos()
        {
            _playerManager.SetCurrentPlayerPosition(_tutorialLevel.StartPos);

        }

        public void RetryTutorial()
        {
            ExitAndMoveToScene("TutorialScene");
        }

        public void OpenSceneExitPanel()
        {
            _exitPanel.Open();
        }

        public void ExitAndMoveToScene(string sceneName)
        {
            StartCoroutine(ExitScene(sceneName));

        }

        private IEnumerator ExitScene(string sceneName)
        {
            OpenSceneExitPanel();
            yield return new WaitForSeconds(_sceneExitDelay);
            SceneManager.LoadScene(sceneName);
        }



    }

}