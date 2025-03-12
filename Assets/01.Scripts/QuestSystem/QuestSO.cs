using QuestSystem.LevelSystem;
using QuestSystem.QuestTarget;
using UnityEngine;
namespace QuestSystem
{
    [CreateAssetMenu(menuName = "SO/QuestSO")]
    public class QuestSO : ScriptableObject
    {
        public int id;
        public QuestType questType;
        public string questName;
        public string description;
        // 목표 설정
        public float startProgress = 0f; //초기 진행상황. 보통의 경우 0임
        public float goalProgress;
        public TargetInfoSO[] targetInfoList;
        public LevelDataSO levelData;

        #region External Functions

        public bool CheckCorrectTarget(string targetCode)
        {
            for (int i = 0; i < targetCode.Length; i++)
            {
                if(targetInfoList[i].targetName.Equals(targetCode))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetTargetIndex(string targetCode)
        {
            for (int i = 0; i < targetCode.Length; i++)
            {
                if(targetInfoList[i].targetName.Equals(targetCode))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion


    }
}