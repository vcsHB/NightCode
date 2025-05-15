using Basement;
using Core.StageController;
using System;
using System.Collections.Generic;
using System.IO;
using Tutorial;
using UnityEngine;

namespace Base.Office
{
    public class OfficeManager : MonoSingleton<OfficeManager>
    {
        public uint Money { get; private set; } = 0;
        public event Action onGetMoney, onLoseMoney;

        public OfficeSO office;
        public OfficeNPC an, jinLay;
        public CheckSkipStagePanel checkSkipStagePanel;

        //public MissionSelectPanel missionSelectPanel;

        private string _path = Path.Combine(Application.dataPath, "Save/OfficeSave.json");
        private string _folderPath = Path.Combine(Application.dataPath, "Save");

        protected override void Awake()
        {
            base.Awake();
            Load();
        }


        public void Init(OfficeSO office)
        {
            this.office = office;
            an.Init(office.ANInfo);
            jinLay.Init(office.JinLayInfo);
            
            if (office.stageToSkip != null)
            {
                checkSkipStagePanel.stageToSkip = office.stageToSkip;
                checkSkipStagePanel.Open();
            }
        }


        #region Money

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

        #endregion


        #region Save&Load

        public void Save()
        {
            DirectoryInfo di = new DirectoryInfo(_folderPath);

            if (di.Exists == false)
                di.Create();

            OfficeSave save = new OfficeSave(Money);
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
        }

        #endregion
    }

    public class OfficeSave
    {
        public uint money;

        public OfficeSave(uint money)
        {
            this.money = money;
        }
    }
}
