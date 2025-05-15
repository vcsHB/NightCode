using Base.Cafe;
using Base.Office;
using Office;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.StageController
{
    public class StageManager : MonoSingleton<StageManager>
    {
        public StageSetSO stageSet;
        [HideInInspector] public StageSO currentStage;

        //SerailizeField is for debugind
        [SerializeField] private int _stageProgress = 0;
        private StageLoadingPanel stageLoadingPanel;
        private string _path = Path.Combine(Application.dataPath, "Save/StageSave.json");
        private string _folderPath = Path.Combine(Application.dataPath, "Save");


        protected override void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                stageLoadingPanel = GetComponentInChildren<StageLoadingPanel>();

                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GoToTitle()
        {
            stageLoadingPanel.onCompleteOpenPanel += LoadTitle;
            stageLoadingPanel.Open();
        }

        private void LoadTitle()
        {
            SceneManager.LoadScene(SceneName.TitleScene);

            stageLoadingPanel.Close();
            stageLoadingPanel.onCompleteOpenPanel -= LoadTitle;
        }

        public StageSO GetNextStage()
        {
            if (stageSet.stageList.Count <= _stageProgress + 1) return null;
            return stageSet.stageList[_stageProgress + 1];
        }

        public void LoadCurrentScene()
        {
            if (currentStage == null)
            {
                currentStage = stageSet.stageList.Find(stage => stage.isFirstStage);
                _stageProgress = currentStage.id;
            }
            else currentStage = stageSet.stageList.Find(stage => stage.id == _stageProgress);

            stageLoadingPanel.onCompleteOpenPanel += LoadStage;
            stageLoadingPanel.onCompletClosePanel += InitStage;
            stageLoadingPanel.Open();
        }

        public void LoadScene()
        {
            stageLoadingPanel.onCompleteOpenPanel += LoadStage;
            stageLoadingPanel.onCompletClosePanel += InitStage;
            stageLoadingPanel.Open();
        }

        public void LoadNextStage()
        {
            if (currentStage.nextStage == null)
            {
                Debug.LogError("NextStageIsNotExsist");
                return;
            }

            currentStage = currentStage.nextStage;
            _stageProgress = currentStage.id;
            stageLoadingPanel.onCompleteOpenPanel += LoadStage;
            stageLoadingPanel.onCompletClosePanel += InitStage;
            stageLoadingPanel.Open();
        }

        public void ReloadCurrentScene()
        {
            stageLoadingPanel.onCompleteOpenPanel += LoadStage;
            stageLoadingPanel.onCompletClosePanel += InitStage;
            stageLoadingPanel.Open();
        }

        private void InitStage()
        {
            if (currentStage is CafeStageSO cafeStage)
                CafeManager.Instance.Init(cafeStage.cafeInfo);

            if (currentStage is OfficeStageSO officeStage)
                OfficeManager.Instance.Init(officeStage.officeInfo);

            stageLoadingPanel.onCompletClosePanel -= InitStage;
        }

        private void LoadStage()
        {
            SceneManager.LoadScene(currentStage.sceneName);

            stageLoadingPanel.Close();
            stageLoadingPanel.onCompleteOpenPanel -= LoadStage;
            Save();
        }

        public void Save()
        {
            DirectoryInfo directiory = new DirectoryInfo(_folderPath);

            if (directiory.Exists == false)
                directiory.Create();

            if (File.Exists(_path) == false)
            {
                currentStage = stageSet.stageList.Find(stage => stage.isFirstStage);
                _stageProgress = currentStage.id;
            }

            StageSave save = new StageSave();
            save.currentStage = _stageProgress;

            string json = JsonUtility.ToJson(save);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
                Save();

            string json = File.ReadAllText(_path);
            StageSave save = JsonUtility.FromJson<StageSave>(json);

            _stageProgress = save.currentStage;
            currentStage = stageSet.stageList[_stageProgress];
        }
    }

    [Serializable]
    public class StageSave
    {
        public int currentStage;
    }
}
