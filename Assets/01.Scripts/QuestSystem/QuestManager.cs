using System;
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

        public float ProgressRatio => currentProgress / goalProgress;
    }

    public class QuestManager : MonoBehaviour
    {
        [Header("Events Setting")]
        public UnityEvent OnQuestCompleteEvent;
        public event Action<float, float> OnQuestProgressChangedEvent; // <Progress, GoalProgress>

        [Header("Essential Setting")]
        [SerializeField] private QuestListSO _listSO;
        [Header("Current Status")]
        [field: SerializeField] public QuestSO CurrentQuest { get; private set; }
        [SerializeField] private QuestData _currentQuestData;
        public QuestData CurrentQuestData => _currentQuestData;
        public bool IsComplete => _currentQuestData.currentProgress >= _currentQuestData.goalProgress;



        public void Initialize()
        {

        }


        public void ChangeQuest(QuestSO questSO)
        {
            CurrentQuest = questSO;
            _currentQuestData.questType = questSO.questType;
            _currentQuestData.currentProgress = 0f;
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
        }

        public void IncreaseProgress(float amount)
        {
            _currentQuestData.currentProgress += amount;
            InvokeQuestProgressChanged();
            if (IsComplete)
            {
                OnQuestCompleteEvent?.Invoke();
            }
        }

        private void InvokeQuestProgressChanged()
        {
            OnQuestProgressChangedEvent?.Invoke(_currentQuestData.currentProgress, _currentQuestData.goalProgress);
        }
    }
}