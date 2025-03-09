using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Basement.Training
{
    public class BasementTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        private Tween _tween;
        private RectTransform _rectTrm => transform as RectTransform;

        private void Update()
        {
            SetTimer();
        }

        public void SetTimer()
        {
            Time time = TrainingManager.Instance.CurrentTime;
            _timerText.SetText($"{(time.hour < 12 ? "AM" : "PM")} {time.hour % 12} : {string.Format("{0,2:D2}", time.minute)}");
        }

        public void AddTime(int minute)
        {
            TrainingManager.Instance.AddMinute(minute);
            SetTimer();
        }

        public void AddMinimumTime()
        {
            int minimum = int.MaxValue;

            foreach(CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if(TrainingManager.Instance.TryGetTrainingInfo(character, out TrainingInfo info))
                {
                    if (info.remainTime < minimum)
                        minimum = info.remainTime;
                }
            }

            if (minimum > 6000) minimum = 0;

            TrainingManager.Instance.AddMinute(minimum);
            SetTimer();
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosY(480, 0.3f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosY(600, 0.3f);
        }
    }
}
