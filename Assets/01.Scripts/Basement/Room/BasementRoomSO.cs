using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/Room")]
    public class BasementRoomSO : ScriptableObject
    {
        public string roomName;

        public BasementRoom roomPf;
        public int requireMoney;
    }

    public enum TestResourceType
    {
        Wood,
        Stone
    }
}
