using Basement.Mission;
using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        public MissionSelectPanel missionPanel;
        public Furniture table;

        protected override void Awake()
        {
            base.Awake();
            table.Init(this);
            table.InteractAction += missionPanel.Open;
        }

        private void OnDisable()
        {
            table.InteractAction -= missionPanel.Open;
        }

        public override void FocusRoom()
        {
            FocusCamera();
            _isBasementMode = true;
        }
    }
}
