using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Basement
{
    public class BasementManager : MonoSingleton<BasementManager>
    {
        //얘는 그냥 초기 설정인거임
        public BasementSO basementInfo;
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
            => roomPositions[floor].roomPositions[roomNumber];

        public BasementRoom CreateRoom(BasementRoomType roomType, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSet.GetRoomSO(roomType).roomPf, roomTrm);
            room.Init(basement);

            basementInfo.floorInfos[floor].rooms[roomNumber].roomType = roomType;

            return room;
        }

        public BasementRoom CreateRoom(BasementRoomSO roomSO, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSO.roomPf, roomTrm); 
            room.Init(basement);

            basementInfo.floorInfos[floor].rooms[roomNumber].roomType = room.roomType;
            return room;
        }
    }

    [Serializable]
    public class FloorInfo
    {
        public int floor;
        public List<RoomInfo> rooms = new List<RoomInfo>(4);
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
