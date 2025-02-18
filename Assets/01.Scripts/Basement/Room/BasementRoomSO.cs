using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/Room")]
    public class BasementRoomSO : ScriptableObject
    {
        public BasementRoom roomPf;
        public List<RequireResource> requireResource;
    }

    [Serializable]
    public struct RequireResource
    {
        public TestResourceType resourceType;
        public int amount;
    }

    public enum TestResourceType
    {
        Wood,
        Stone
    }
}
