using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Basement
{
    public class BasementController : MonoBehaviour
    {
        public BasementSO basementInfo;

        private string _path = Path.Combine(Application.dataPath, "Basement.json");

        public bool CheckCanExpend()
            => basementInfo.expendedFloor >= basementInfo.maxFloor;

        public void ExpendFloor()
        {

        }

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
            if(File.Exists(_path) == false)
            {
                Save();
                return;
            }

            string json = File.ReadAllText(_path);
            BasementSave save = JsonUtility.FromJson<BasementSave>(json);

            basementInfo.expendedFloor = save.expendedFloor;
            basementInfo.floorInfos = save.floorInfos;
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
        int floor;
        public List<RoomInfo> rooms = new List<RoomInfo>(4);
    }

    [Serializable]
    public struct RoomInfo
    {
        public int level;
        public BasementRoomType roomType;
        public List<FurnitureSO> furnitures;
    }
}
