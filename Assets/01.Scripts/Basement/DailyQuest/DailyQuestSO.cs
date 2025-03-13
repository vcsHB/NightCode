using Basement.Training;
using System;
using UnityEngine;

namespace Basement.Quest
{
    [CreateAssetMenu(menuName = "SO/Basement/DailyQuest")]
    public class DailyQuestSO : ScriptableObject
    {
        public string questName;
        [TextArea] public string questexplain;

        public CharacterEnum characterType;
        public BasementRoomSO questRoom;
        public RewardStruct reward;
    }

    [Serializable]
    public struct RewardStruct
    {
        public bool isRewardVeined;
        //나중에 매력도 상승 그런거도 있게
        public SkillPointEnum skillPointType;
        public int point;
    }
}
