using TMPro;
using UnityEngine;
namespace SpeedRun
{

    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public void HandleRefreshTimer(float time)
        {
            int m = (int)time / 60;
            time -= m;
            _timerText.text = $"{m.ToString("00")}:{time.ToString("00.00")}";
        }
    }
}