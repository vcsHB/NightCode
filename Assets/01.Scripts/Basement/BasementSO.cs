using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/BasementSO")]
    public class BasementSO : ScriptableObject
    {
        public readonly int maxFloor = 4;
        public int expendedFloor = 1;

        //public List<FloorInfo> floorInfos = new List<FloorInfo>();
    }

    public enum BasementRoomType
    {
        Empty,              // 없음
        Cafe,               // 카페
        Office,             //사무실?
        TrainingRoom,       // 훈련소
        Lodging,            // 숙소
    }
}
