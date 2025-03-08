using Basement.Mission;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public Furniture table;
        private OfficeUI _officeUI;

        protected override void Start()
        {
            table.Init(this);
            table.InteractAction += InteractTable;
            _officeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Office) as OfficeUI;
            Init(BasementController);
        }

        private void OnDisable()
        {
            table.InteractAction -= InteractTable;
        }

        private void InteractTable()
        {
            _officeUI.Open();
        }

        public override void FocusRoom()
        {
            bool isComplete = WorkManager.Instance.CurrentTime.hour >= WorkManager.Instance.endTime.hour;

            if (isComplete)
            {
                FocusCamera();
            }
            else
            {
                base.FocusRoom();
            }
        }

        protected override void CloseUI()
        {
            _officeUI.Close();
        }
    }
}
