using Basement.Quest;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public Furniture table;
        [SerializeField] private List<DailyQuestSO> _questList;

        private OfficeUI _officeUI;
        public OfficeUI OfficeUI
        {
            get
            {
                if (_officeUI == null)
                    _officeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Office) as OfficeUI;
                return _officeUI;
            }
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

            FocusCamera();
            OpenRoomUI();
        }

        private void InteractTable()
        {
            OfficeUI.Open();
            RoomUI.Close();
            //UIManager.Instance.returnButton.AddReturnAction(OfficeUI.Close);
            UIManager.Instance.basementUI.Close();
        }

        public override void FocusRoom()
        {
            bool isComplete = WorkManager.Instance.CurrentTime.hour >= WorkManager.Instance.endTime.hour;

            if (isComplete) FocusCamera();
            else
            {
                base.FocusRoom();
                RoomUI.SetOppositeUI(OfficeUI);
            }
        }

        public override void CloseUI()
        {
            OfficeUI.Close();
            UIManager.Instance.basementUI.Open();
        }
    }
}
