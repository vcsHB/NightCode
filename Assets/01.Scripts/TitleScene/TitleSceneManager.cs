using Core;
using Core.DataControl;
using Core.StageController;
using DG.Tweening;
using System.IO;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{

    public class TitleSceneManager : MonoBehaviour
    {
        [Header("Game Init Setting")]

        [SerializeField] private LogController _logController;
        [SerializeField] private LogContent[] _initLogList;
        [SerializeField] private LogContent _denyStartGameLog;

        [Space(10f)]
        [Header("Essential Settings")]

        [SerializeField] private string _startConnectSceneName = "CafeScene";
        [SerializeField] private string _tutorialSceneName = "TutorialScene";
        private string _folderPath = Path.Combine(Application.dataPath, "Save");
        private bool _isReady;

        public void HandleStart()
        {
            if (!_isReady)
            {
                _logController.SendLog(_denyStartGameLog);
                return;
            }

            if (DataLoader.Instance.GetUserData().isClearTutorial)
            {
                SceneManager.LoadSceneAsync(_startConnectSceneName);
            }
            else
                SceneManager.LoadSceneAsync(_tutorialSceneName);
        }

        public void HandleStartBoss()
        {
            // StageManager.Instance.currentStage = _bossStage;
            // StageManager.Instance.LoadScene();
        }
        public void HandleMoveSpeedRun()
        {
            SceneManager.LoadScene(SceneName.SpeedRunScene);
        }
        public void ResetData()
        {
            if (Directory.Exists(_folderPath))
            {
                string[] files = Directory.GetFiles(_folderPath);

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }

        public void HandleQuit()
        {
            Application.Quit();
        }

        public void SendInitLogs()
        {

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < _initLogList.Length; i++)
            {
                LogContent content = _initLogList[i];
                sequence.AppendCallback(() => _logController.SendLog(content));
                sequence.AppendInterval(content.term);
            }
            sequence.OnComplete(() => _isReady = true);

        }
    }

}