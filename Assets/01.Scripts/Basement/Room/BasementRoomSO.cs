using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/Room")]
    public class BasementRoomSO : ScriptableObject
    {
        public string roomName;
        public string roomExplain;

        public BasementRoom roomPf;
        public int maxSeatingCapacity;
        public int requireMoney;
    }

    public enum TestResourceType
    {
        Wood,
        Stone
    }
}
