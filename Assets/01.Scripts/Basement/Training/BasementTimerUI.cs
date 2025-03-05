using System.Collections.Generic;
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
            _timerText.SetText($"{(time.hour < 13 ? "AM" : "PM")} {time.hour % 12 + 1} : {string.Format("{0,2:D2}", time.minute)}");
        }

        public void AddTime(int minute)
        {
            TrainingManager.Instance.AddMinute(minute);
            SetTimer();
        }

        public void AddMinimumTime()
        {
            int minimum = int.MaxValue;

            List<TrainingInfo> infoList = TrainingManager.Instance.characterTrainingInfo.Values.ToList();
            infoList.ForEach(info =>
            {
                if (info.remainTime < minimum)
                    minimum = info.remainTime;
            });

            if (infoList.Count == 0) minimum = 0;

            TrainingManager.Instance.AddMinute(minimum);
            SetTimer();
        }
    }
}
