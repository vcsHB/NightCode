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
        [SerializeField] private UIPanel[] _initUIs;
        [SerializeField] private float _uiEnableTerm = 0.2f;

        [Space(10f)]
        [Header("Essential Settings")]

        [SerializeField] private StageSO _bossStage;
        [SerializeField] private string _startConnectSceneName = "CafeScene";
        [SerializeField] private string _tutorialSceneName = "TutorialScene";
        private string _folderPath = Path.Combine(Application.dataPath, "Save");
        private bool _isReady;
        private void Start()
        {
            SetEnableInitUIs();
            SendInitLogs();
        }
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

        private void SetEnableInitUIs()
        {
            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < _initUIs.Length; i++)
            {
                sequence.AppendCallback(_initUIs[i].Open);
                sequence.AppendInterval(_uiEnableTerm);
            }
        }

        private void SendInitLogs()
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