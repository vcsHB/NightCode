using Basement.Training;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Basement
{
    public class WorkManager : MonoSingleton<WorkManager>
    {
        public Cafe cafe;

        [SerializeField] private BasementTime startTime;
        [SerializeField] private BasementTime endTime;
        private BasementTime currentTime;

        private Dictionary<CharacterEnum, RoomActionInfo> characterTrainingInfo;
        public BasementTime CurrentTime => currentTime;

        protected override void Awake()
        {
            base.Awake();
            characterTrainingInfo = new Dictionary<CharacterEnum, RoomActionInfo>();
            currentTime = startTime;
        }

        public void AddMinute(int minute)
        {
            currentTime.AddMinute(minute);

            if (currentTime.hour >= endTime.hour)
            {
                BasementManager.Instance.basement.CompleteScadule();
                return;
            }

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (characterTrainingInfo.TryGetValue(character, out RoomActionInfo info))
                {
                    info.remainTime -= minute;
                    characterTrainingInfo[character] = info;

                    if (info.remainTime <= 0)
                    {
                        info.completeAction?.Invoke(character);
                        RemoveRoomAction(character);
                    }
                }
            }

            cafe.PassTime(minute);
        }

        public void AddRoomAction(CharacterEnum character, int requireTime, string displayAction, Action<CharacterEnum> completeAction = null)
        {
            RoomActionInfo trainingInfo = new RoomActionInfo();
            trainingInfo.completeAction = completeAction;
            trainingInfo.displayAction = displayAction;
            trainingInfo.remainTime = requireTime;
            trainingInfo.startTime = currentTime;
            trainingInfo.isStartWork = false;

            characterTrainingInfo.Add(character, trainingInfo);
        }

        public void RemoveRoomAction(CharacterEnum character)
        {
            characterTrainingInfo.Remove(character);
        }

        public bool TryGetTrainingInfo(CharacterEnum character, out RoomActionInfo info)
            => characterTrainingInfo.TryGetValue(character, out info);

        public bool CheckWorking(CharacterEnum character)
        {
            bool isWorking = (cafe.isCafeOpen == true && cafe.PositionedCharacter == character)
                || TryGetTrainingInfo(character, out RoomActionInfo info);

            return isWorking;
        }

    }

    public struct RoomActionInfo
    {
        public Action<CharacterEnum> completeAction;
        public BasementTime startTime;
        public int remainTime;
        public string displayAction;

        public bool isStartWork;
    }

    [Serializable]
    public class BasementTime
    {
        public int hour;
        public int minute;

        public BasementTime(int h, int m)
        {
            hour = h;
            minute = m;
        }

        public void AddMinute(int time)
        {
            minute += time;
            hour += minute / 60;
            minute %= 60;
        }

        public string ToTimeText()
        {
            string AMPM = (hour < 12 ? "AM" : "PM");
            int h = (hour == 12 ? hour : hour % 12);

            string timeText = $"{AMPM} {h}:{string.Format("{0,2:D2}", minute)}";
            return timeText;
        }
    }
}
