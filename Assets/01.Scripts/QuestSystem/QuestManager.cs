using System;
using QuestSystem.QuestTarget;
using UnityEngine;

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
        [SerializeField] private QuestTargetObject[] _targets;
        [SerializeField] private QuestData _currentQuestData;
        public QuestData CurrentQuestData => _currentQuestData;
        public bool IsComplete => _currentQuestData.currentProgress >= _currentQuestData.goalProgress;
        [Header("Current Status")]
        [field: SerializeField] public QuestSO CurrentQuest { get; private set; }
        
        private void Awake()
        {
            _currentQuestData.questType = CurrentQuest.questType;
            _currentQuestData.currentProgress = CurrentQuest.startProgress;
            _currentQuestData.goalProgress = CurrentQuest.goalProgress;

            InitTargets();
        }


        private void InitTargets()
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].OnTargetCompleteEvent += HandleTargetComplete;
            }
        }

        private void HandleTargetComplete(QuestTargetData data)
        {
            _currentQuestData.currentProgress ++;
        }
    }
}