using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Office
{
    public class OfficeManager : MonoSingleton<OfficeManager>
    {
        public uint Money { get; private set; } = 0;
        public event Action onGetMoney, onLoseMoney;

        public MissionSetSO missionSet;
        public MissionSelectPanel missionSelectPanel;
        [HideInInspector] public List<MissionSO> _currentMission = new List<MissionSO>();

        private string _path = Path.Combine(Application.dataPath, "Save/OfficeSave.json");
        private string _folderPath = Path.Combine(Application.dataPath, "Save");

        protected override void Awake()
        {
            base.Awake();

            Load();
        }


        public void GetMoney(uint amount)
        {
            Money += amount;
            onGetMoney?.Invoke();
        }

        public void LoseMoney(uint amount)
        {
            Money -= amount;
            onLoseMoney?.Invoke();
        }



        #region MissionRegion

        public void AddMission(ushort missionId)
        {
            MissionSO mission = missionSet.missionList.Find(mission => mission.id == missionId);
            AddMission(mission);
        }

        public void AddMission(MissionSO mission)
        {
            if (_currentMission.Exists(m => mission.id == m.id)) return;

            _currentMission.Add(mission);
            missionSelectPanel.AddMission(mission);
        }

        public void RemoveMission(int missionId)
        {
            MissionSO mission = missionSet.missionList.Find(mission => mission.id == missionId);
            RemoveMission(mission);
        }

        public void RemoveMission(MissionSO mission)
        {
            if (_currentMission.Exists(m => mission.id == m.id) == false) return;

            _currentMission.Remove(mission);
            missionSelectPanel.RemoveMission(mission);
        }


        public void ClearMission(MissionSO mission)
        {
            //DO NOT CHANGE ORDER
            RemoveMission(mission);
            mission.nextMissions.ForEach(m =>  AddMission(m));

            Save();
        }

        public void ClearMission(int missionId)
        {
            MissionSO mission = missionSet.missionList.Find(mission => mission.id == missionId);
            ClearMission(mission);
        }

        #endregion


        #region Save&Load

        public void Save()
        {
            DirectoryInfo di = new DirectoryInfo(_folderPath);

            if (di.Exists == false)
                di.Create();

            if (_currentMission.Count == 0)
                AddMission(0);

            OfficeSave save = new OfficeSave(Money, _currentMission);
            string json = JsonUtility.ToJson(save);

            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            if (File.Exists(_path) == false)
                Save();

            string json = File.ReadAllText(_path);
            OfficeSave save = JsonUtility.FromJson<OfficeSave>(json);

            Money = save.money;
            _currentMission = save.GetCurrentMissions(missionSet);
            _currentMission.ForEach(mission =>  missionSelectPanel.AddMission(mission));
            //_currentMission.ForEach(mission => ClearMission(mission));

            if (_currentMission.Count == 0)
                AddMission(0);
        }

        #endregion
    }

    public class OfficeSave
    {
        public uint money;
        public List<ushort> clearedMissions;

        public OfficeSave(uint money, List<MissionSO> clearedMissions)
        {
            this.money = money;
            this.clearedMissions = new List<ushort>();

            clearedMissions.ForEach(mission => this.clearedMissions.Add(mission.id));
        }

        public List<MissionSO> GetCurrentMissions(MissionSetSO missionSet)
        {
            List<MissionSO> missionList = new List<MissionSO>();

            clearedMissions.ForEach(missionId =>
            missionList.Add(missionSet.missionList.Find(mission => mission.id == missionId)));
            return missionList;
        }
    }
}
