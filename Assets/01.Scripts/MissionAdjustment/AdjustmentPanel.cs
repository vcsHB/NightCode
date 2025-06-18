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
            _manager.OnEndMissionEvent += Open;

        }

        void OnDestroy()
        {
            _manager.OnEndMissionEvent -= Open;

        }
        [ContextMenu("DebugOver")]
        public override void Open()
        {
            base.Open();
            _timeDisplayer.SetTimeText(_manager.EndTime);
            _currentPointText.text = $"{_manager.CurrentPoint.ToString()} 획득";

        }

    }
}