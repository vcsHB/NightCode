using Basement.Mission;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public OfficeUI officeUI;
        public Furniture table;

        protected override void Awake()
        {
            base.Awake();
            table.Init(this);
            table.InteractAction += InteractTable;
        }

        private void OnDisable()
        {
            table.InteractAction -= InteractTable;
        }

        private void InteractTable()
        {
            officeUI.Open();
        }

        public override void FocusRoom()
        {
            FocusCamera();
            _isBasementMode = true;
        }
    }
}
