using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/BasementRoomSet")]
    public class BasementRoomSetSO : ScriptableObject
    {
        public List<BasementRoom> basementRoomSet;
        public Dictionary<BasementRoomType, BasementRoom> basementRoomDictionary;

        private void OnEnable()
        {
            basementRoomDictionary = new Dictionary<BasementRoomType, BasementRoom>();
            basementRoomSet.ForEach(room =>
            {
                if (room != null)
                    basementRoomDictionary.Add(room.roomType, room);
            });
        }

        public BasementRoom GetRoomPrefab(BasementRoomType roomType)
            => basementRoomDictionary[roomType];
    }
}
