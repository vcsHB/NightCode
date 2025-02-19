using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/BasementRoomSet")]
    public class BasementRoomSetSO : ScriptableObject
    {
        public List<BasementRoomSO> basementRoomSet;
        public Dictionary<BasementRoomType, BasementRoomSO> basementRoomDictionary;

        private void OnEnable()
        {
            basementRoomDictionary = new Dictionary<BasementRoomType, BasementRoomSO>();
            basementRoomSet.ForEach(room =>
            {
                if (room != null)
                    basementRoomDictionary.Add(room.roomPf.roomType, room);
            });
        }

        public BasementRoomSO GetRoomSO(BasementRoomType roomType)
            => basementRoomDictionary[roomType];
    }
}
