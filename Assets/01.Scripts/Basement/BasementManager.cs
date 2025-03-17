using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Basement
{
    public class BasementManager : MonoSingleton<BasementManager>
    {
        public BasementRoomSetSO roomSet;
        public BasementController basement;

        public List<FloorPositionInfo> roomPositions;

        public int money;
        private string _path = Path.Combine(Application.dataPath, "Basement.json");

        public int GetMoney()
            => money;

        public void UseResource(int amount)
            => money -= amount;

        public Transform GetRoomPosition(int floor, int roomNumber)
        {
            if (floor < 1 || floor > 3 || roomNumber < 0 || roomNumber > 2) return null;
            return roomPositions[floor - 1].roomPositions[roomNumber];
        }

        public BasementRoom CreateRoom(BasementRoomType roomType, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSet.GetRoomSO(roomType).roomPf, roomTrm);
            room.Init(basement);

            return room;
        }

        public BasementRoom CreateRoom(BasementRoomSO roomSO, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSO.roomPf, roomTrm); 
            room.Init(basement);
            basement.SetRoom(room, floor, roomNumber);

            return room;
        }
    }

    [Serializable]
    public class RoomInfo
    {
        public int level;
        public BasementRoomType roomType;
        [HideInInspector] public string factor;
    }

    [Serializable]
    public struct FloorPositionInfo
    {
        public List<Transform> roomPositions;
    }
}
