using System.Collections;
using Combat.PlayerTagSystem;
using Core.StageController;
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
            StartCoroutine(ExitScene());

        }

        private IEnumerator ExitScene()
        {
            OpenSceneExitPanel();
            yield return new WaitForSeconds(_sceneExitDelay);
            StageManager.Instance.LoadNextStage();
            //PlayerManager.Instance.
        }



    }

}