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
        [HideInInspector] public StageSO _currentStage;

        //SerailizeField is for debugind
        [SerializeField]private int _stageProgress = 0;
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

        public StageSO GetNextStage()
        {
            if (stageSet.stageList.Count <= _stageProgress + 1) return null;
            return stageSet.stageList[_stageProgress + 1];
        }

        public void LoadNextStage()
        {
            stageLoadingPanel.onCompleteOpenPanel += LoadStage;
            stageLoadingPanel.Open();
        }

        private void LoadStage()
        {
            _currentStage = stageSet.stageList[++_stageProgress];
            AsyncOperation loadHandle = SceneManager.LoadSceneAsync(_currentStage.sceneName);
            loadHandle.completed += (handle) =>
            {
                stageLoadingPanel.Close();

                if (_currentStage is CafeStageSO cafeStage)
                    CafeManager.Instance.Init(cafeStage.cafeInfo);

                //if (_currentStage is OfficeStageSO officeStage)
                //    OfficeManager.Instance.Init(officeStage.officeInfo);
            };
            stageLoadingPanel.onCompleteOpenPanel -= LoadStage;
            Save();
        }

        public void Save()
        {
            DirectoryInfo directiory = new DirectoryInfo(_folderPath);

            if (directiory.Exists == false)
                directiory.Create();

            if (_currentStage == null)
                _currentStage = stageSet.stageList[0];

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
            _currentStage = stageSet.stageList[_stageProgress];
            // _currentMission.ForEach(mission =>  missionSelectPanel.AddMission(mission));
            //_currentMission.ForEach(mission => ClearMission(mission));

            if (_currentStage == null)
                _currentStage = stageSet.stageList[0];
        }
    }

    [Serializable]
    public class StageSave
    {
        public int currentStage;
    }
}
