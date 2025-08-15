using TMPro;
using UI;
using UnityEngine;
namespace SpeedRun
{

    public class TimerUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public void SetTimerColor(Color color)
        {
            _timerText.color = color;
        }

        public void HandleRefreshTimer(float time)
        {
            int m = (int)time / 60;
            float sec = time  % 60;
            _timerText.text = $"{m.ToString("00")}:{sec.ToString("00.00")}";
        }
    }
}