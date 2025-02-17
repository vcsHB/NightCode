using Basement;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Basement
{
    public class BasementManager : MonoSingleton<BasementManager>
    {
        //얘는 그냥 초기 설정인거임
        public BasementSO basementInfo;
        public BasementRoomSetSO roomSet;

        public List<BasementPlayerFollowTarget> floorCameraTarget;
        public List<FloorPositionInfo> roomPositions;
        public List<FloorRoomInfo> roomInfos;

        private string _path = Path.Combine(Application.dataPath, "Basement.json");

        protected override void Awake()
        {
            base.Awake();
            Load();
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
                Save();
        }

        public bool CheckCanExpend()
            => basementInfo.expendedFloor >= basementInfo.maxFloor;

        public Transform GetCameraTarget(int floor) => floorCameraTarget[floor].transform;

        public Transform GetRoomPosition(int floor, int roomNumber)
            => roomPositions[floor].roomPositions[roomNumber];

        public BasementRoom CreateRoom(BasementRoomType roomType, int floor, int roomNumber)
        {
            Transform roomTrm = GetRoomPosition(floor, roomNumber);
            BasementRoom room = Instantiate(roomSet.GetRoomPrefab(roomType), roomTrm);

            roomInfos[floor].roomInfos[roomNumber] = room;
            basementInfo.floorInfos[floor].rooms[roomNumber].roomType = roomType;

            return room;
        }

        #region Save&Load

        public void Save()
        {
            if(roomInfos != null)
            {
                for (int i = 0; i < basementInfo.floorInfos.Count; i++)
                {
                    FloorInfo info = basementInfo.floorInfos[i];
                    info.floor = i;

                    for (int j = 0; j < info.rooms.Count; j++)
                    {
                        RoomInfo room = info.rooms[j];
                        if (roomInfos[i]?.roomInfos[j] != null)
                            room.factor = roomInfos[i].roomInfos[j].GetFactor();
                    }
                }
            }

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

            //roomInfos 방 개수에 맞게 초기화 해주기
            roomInfos = new List<FloorRoomInfo>();
            for (int i = 0; i < basementInfo.maxFloor; i++)
            {
                FloorRoomInfo roomInfo = new FloorRoomInfo();
                roomInfo.roomInfos = new List<BasementRoom>();

                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                    roomInfo.roomInfos.Add(null);

                roomInfos.Add(roomInfo);
            }

            basementInfo = ScriptableObject.CreateInstance<BasementSO>();
            string json = File.ReadAllText(_path);
            BasementSave save = JsonUtility.FromJson<BasementSave>(json);

            basementInfo.expendedFloor = save.expendedFloor;
            basementInfo.floorInfos = save.floorInfos;

            for (int i = 0; i <= basementInfo.expendedFloor; i++)
            {
                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                {
                    RoomInfo roomInfo = basementInfo.floorInfos[i].rooms[j];

                    if ((int)roomInfo.roomType < 3) continue;

                    BasementRoom room = CreateRoom(roomInfo.roomType, i, j);
                    room.SetFactor(roomInfo.factor);
                    roomInfos[i].roomInfos[j] = room;
                    //Debug.Log(roomInfo.roomType);
                    //Transform positionTrm = buildUI.basementBuildUI[i].roomBuildUI[j];
                    //BasementRoom _room = Instantiate(basementInfo.GetBasementRoom(roomInfo.roomType));
                    //_room.transform.SetPositionAndRotation(positionTrm.position, Quaternion.identity);

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
