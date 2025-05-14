using MissionAdjust;
using SpeedRun;
using TMPro;
using UI.Common;
using UnityEngine;
namespace UI.InGame.GameUI.AdjustmentSystem
{

    public class AdjustmentPanel : UIPanel
    {
        [SerializeField] private AdjustmentManager _manager;
        [SerializeField] private TextMeshProUGUI _currentPointText;
        [SerializeField] private TimeDisplayer _timeDisplayer;
        protected override void Awake()
        {
            base.Awake();

        }
        [ContextMenu("DebugOver")]
        public override void Open()
        {
            base.Open();
            _manager.ClearMission();
            _timeDisplayer.SetTimeText(_manager.EndTime);
            _currentPointText.text = $"{_manager.CurrentPoint.ToString()} 획득";

        }

    }
}