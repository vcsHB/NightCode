using System.Linq;
using TMPro;
using UnityEngine;

namespace Basement.Training
{
    public class BasementTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private void Update()
        {
            SetTimer();
        }

        public void SetTimer()
        {
            Time time = TrainingManager.Instance.CurrentTime;
            _timerText.SetText($"{(time.hour < 13? "AM":"PM")} {time.hour % 12 + 1} : {string.Format("{0,2:D2}", time.minute) }");
        }

        public void AddTime(int minute)
        {
            TrainingManager.Instance.CurrentTime.AddMinute(minute);
            SetTimer();
        }

        public void AddMinimumTime()
        {
            TrainingInfo minimum = TrainingManager.Instance.characterTrainingInfo.Values.Min();
            TrainingManager.Instance.CurrentTime.AddMinute(minimum.remainTime);
            SetTimer();
        }
    }
}
