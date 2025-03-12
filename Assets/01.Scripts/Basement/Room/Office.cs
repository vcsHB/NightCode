using Basement.Quest;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public Furniture table;
        private OfficeUI _officeUI;
        [SerializeField]private List<DailyQuestSO> _questList;

        protected override void Start()
        {
            base.Start();
            _officeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Office) as OfficeUI;
            FocusRoom();
        }

        private void OnDisable()
        {
            table.InteractAction -= InteractTable;
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            table.Init(this);
            table.InteractAction += InteractTable;
        }

        private void InteractTable()
        {
            _officeUI.Open();
        }

        public override void FocusRoom()
        {
            bool isComplete = WorkManager.Instance.CurrentTime.hour >= WorkManager.Instance.endTime.hour;
            Debug.Log("¾ßÇã");

            if (isComplete) FocusCamera();
            else base.FocusRoom();
        }

        public override void CloseUI()
        {
            _officeUI.Close();
        }

        public override void OpenUI()
        {
            _roomUI.SetRoom(this);
            _roomUI.Open();
        }
    }
}
