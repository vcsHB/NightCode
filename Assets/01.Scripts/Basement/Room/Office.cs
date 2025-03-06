using Basement.Mission;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public MissionSelectPanel missionPanel;
        public CharacterSelectPanel selectaPanel;
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
            missionPanel.Open();
            selectaPanel.Open(Vector2.zero);
        }

        public override void FocusRoom()
        {
            FocusCamera();
            _isBasementMode = true;
        }
    }
}
