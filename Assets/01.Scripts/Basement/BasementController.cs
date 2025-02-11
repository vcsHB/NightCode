using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Basement
{
    public class BasementController : MonoBehaviour
    {
        public BasementSO basementInfo;
        public BasementBuildUI buildUI;

        private string _path = Path.Combine(Application.dataPath, "Basement.json");

        private void OnEnable()
        {
            Load();
        }

        public bool CheckCanExpend()
            => basementInfo.expendedFloor >= basementInfo.maxFloor;

        public void Save()
        {
            BasementSave save = new BasementSave();
            save.expendedFloor = basementInfo.expendedFloor;
            save.floorInfos = basementInfo.floorInfos;

            string json = JsonUtility.ToJson(save);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
            {
                Save();
                return;
            }

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

                    Debug.Log(roomInfo.roomType);
                    //Transform positionTrm = buildUI.basementBuildUI[i].roomBuildUI[j];
                    //BasementRoom room = Instantiate(basementInfo.GetBasementRoom(roomInfo.roomType));
                    //room.transform.SetPositionAndRotation(positionTrm.position, Quaternion.identity);

                    //가구 소환
                }
            }
        }
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
}
