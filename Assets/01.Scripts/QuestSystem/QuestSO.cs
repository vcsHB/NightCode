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
        public string[] targetCodeList;

        #region External Functions

        public bool CheckCorrectTarget(string targetCode)
        {
            for (int i = 0; i < targetCode.Length; i++)
            {
                if(targetCodeList[i].Equals(targetCode))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


    }
}