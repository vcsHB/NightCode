using Basement;
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

        public List<Transform> floorCameraTarget;
        public List<FloorPositionInfo> roomPositions;
        public List<FloorRoomInfo> roomInfos;

        private string _path = Path.Combine(Application.dataPath, "Basement.json");

        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        public bool CheckCanExpend()
            => basementInfo.expendedFloor >= basementInfo.maxFloor;

        public Transform GetCameraTarget(int floor) => floorCameraTarget[floor];

        public Transform GetRoomPosition(int floor, int roomNumber)
            => roomPositions[floor].roomPositions[roomNumber];

        public void CreateRoom(BasementRoomType roomType, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSet.GetRoomPrefab(roomType), roomTrm);

            roomInfos[floor].roomInfos[roomNumber] = room;
            basementInfo.floorInfos[floor].rooms[roomNumber].roomType = roomType;
        }

        #region Save&Load

        public void Save()
        {
            BasementSave save = new BasementSave();
            save.expendedFloor = basementInfo.expendedFloor;
            save.floorInfos = basementInfo.floorInfos;

            string json = JsonUtility.ToJson(save);
            File.WriteAllText(_path, json);

            Load();
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                Save();
                return;
            }

            roomInfos = new List<FloorRoomInfo>();
            for (int i = 0; i < basementInfo.maxFloor; i++)
            {
                FloorRoomInfo roomInfo = new FloorRoomInfo();
                roomInfo.roomInfos = new List<BasementRoom>();

                for(int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                    roomInfo.roomInfos.Add(null);

                roomInfos.Add(roomInfo);
            }

            basementInfo = ScriptableObject.CreateInstance<BasementSO>();
            string json = File.ReadAllText(_path);
            BasementSave save = JsonUtility.FromJson<BasementSave>(json);

            basementInfo.expendedFloor = save.expendedFloor;
            basementInfo.floorInfos = save.floorInfos;

            for (int i = 0; i < basementInfo.expendedFloor; i++)
            {
                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                {
                    RoomInfo roomInfo = basementInfo.floorInfos[i].rooms[j];

                    if ((int)roomInfo.roomType < 3) continue;

                    CreateRoom(roomInfo.roomType, i, j);
                    //Debug.Log(roomInfo.roomType);
                    //Transform positionTrm = buildUI.basementBuildUI[i].roomBuildUI[j];
                    //BasementRoom room = Instantiate(basementInfo.GetBasementRoom(roomInfo.roomType));
                    //room.transform.SetPositionAndRotation(positionTrm.position, Quaternion.identity);

                    //가구 소환
                }
            }
        }

        #endregion
    }

    [Serializable]
    public class BasementSave
    {
        public int expendedFloor;
        public List<FloorInfo> floorInfos;
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
        public List<FurnitureSO> furnitures;
        [HideInInspector] public string factor;
    }

    [Serializable]
    public struct FloorPositionInfo
    {
        public List<Transform> roomPositions;
    }

    public class FloorRoomInfo
    {
        public List<BasementRoom> roomInfos;
    }
}
