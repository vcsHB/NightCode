using System;
using QuestSystem.LevelSystem.SpeedRun;
using SpeedRun;
using UnityEngine;
namespace QuestSystem.LevelSystem
{
    public class SpeedRunLevelMap : LevelMap
    {
        [SerializeField] private SpeedRunTimecontroller _timeController;
        [SerializeField] private TimerUI _timer;

        private void Awake()
        {
            _timeController.OnTimeChangeEvent += HandleRefreshTimer;
        }

        private void HandleRefreshTimer(float currentTime, float limitedTime)
        {
            _timer.HandleRefreshTimer(currentTime);
            // TODO: TimerColor Set
        }

        public void StartSpeedRun()
        {
            _timeController.StartSpeedRun();
            _timer.HandleRefreshTimer(0f);
            _timer.Open();
        }


        public void StopSpeedRun()
        {
            _timeController.StopSpeedRun();
            _timer.Close();
        }



    }
}