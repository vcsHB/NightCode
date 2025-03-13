using System;
using QuestSystem.LevelSystem;
using UI.InGame.SystemUI.AlertSystem;
using UnityEngine;
using UnityEngine.Events;
namespace QuestSystem
{
    [System.Serializable]
    public struct QuestData
    {
        public QuestType questType;
        public float goalProgress;
        public float currentProgress;
        public bool[] clearList;

        public float ProgressRatio => currentProgress / goalProgress;
    }

    public class QuestManager : MonoBehaviour
    {
        [Header("Events Setting")]
        public UnityEvent OnQuestCompleteEvent;
        public event Action<float, float> OnQuestProgressChangedEvent; // <Progress, GoalProgress>
        public event Action<QuestSO> OnQuestChangeEvent;
        [Header("Essential Setting")]
        [SerializeField] private QuestListSO _listSO;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private AlertGroup _alertGroup;
        [Header("Current Status")]
        [field: SerializeField] public QuestSO CurrentQuest { get; private set; }
        [SerializeField] private QuestData _currentQuestData;
        public QuestData CurrentQuestData => _currentQuestData;
        public bool IsComplete => _currentQuestData.currentProgress >= _currentQuestData.goalProgress;


        public void Initialize()
        {

        }

        private void Start()
        {
            SetQuest(CurrentQuest);
        }


        public void SetQuest(QuestSO questSO)
        {
            CurrentQuest = questSO;
            _currentQuestData.questType = questSO.questType;
            _currentQuestData.currentProgress = 0f;
            _currentQuestData.clearList = new bool[CurrentQuest.targetInfoList.Length];
            _currentQuestData.goalProgress = CurrentQuest.goalProgress;
            InvokeQuestProgressChanged();

            // 퀘스트 타입에 따른 추가적인 수행처리
            switch (questSO.questType)
            {
                case QuestType.KillSingleTarget:
                    break;
                case QuestType.KillMultiTarget:
                    break;
                case QuestType.KillAll:
                    break;
                case QuestType.Rescue:
                    break;
            }

            if (CurrentQuest.levelData != null)
            {
                _levelController.Initialize(CurrentQuest.levelData);
                _levelController.AddQuestHandler(HandleQuestTargetComplete);
            }
            OnQuestChangeEvent?.Invoke(CurrentQuest);
        }

        public void IncreaseProgress(float amount)
        {
            _currentQuestData.currentProgress += amount;
            InvokeQuestProgressChanged();
            if (IsComplete)
            {
                ClearQuest();
            }
        }
        private void ClearQuest()
        {
            _currentQuestData.currentProgress = _currentQuestData.goalProgress;
            _alertGroup.ShowAlert(CurrentQuest.cleatMessage);
            OnQuestCompleteEvent?.Invoke();
        }

        private void InvokeQuestProgressChanged()
        {
            OnQuestProgressChangedEvent?.Invoke(_currentQuestData.currentProgress, _currentQuestData.goalProgress);
        }

        private void HandleQuestTargetComplete(QuestTargetData completeData)
        {

            float strictProgress = 1 / CurrentQuest.goalProgress;
            switch (CurrentQuest.questType)
            {
                case QuestType.KillSingleTarget:
                    if (CurrentQuest.CheckCorrectTarget(completeData.targetCode))
                    {
                        ClearQuest();
                    }
                    break;
                case QuestType.KillMultiTarget:
                    if (CurrentQuest.CheckCorrectTarget(completeData.targetCode))
                    {
                        _currentQuestData.clearList[CurrentQuest.GetTargetIndex(completeData.targetCode)] = true;
                        IncreaseProgress(strictProgress);
                    }
                    break;
                case QuestType.KillAll:
                    IncreaseProgress(completeData.completeLevel);
                    break;
                case QuestType.Rescue:
                    IncreaseProgress(strictProgress);

                    break;
            }
            //OnQuestChangeEvent?.Invoke(CurrentQuest);
        }
    }
}