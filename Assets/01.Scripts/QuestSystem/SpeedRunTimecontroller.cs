using System;
using UnityEngine;
namespace QuestSystem.LevelSystem.SpeedRun
{

    public class SpeedRunTimecontroller : MonoBehaviour
    {
        public event Action<float, float> OnTimeChangeEvent; // currentTime, LimitedTime
        [SerializeField] private float _limitedTime;
        [SerializeField] private float _currentTime;

        private bool _isSpeedRunStarted;

        private void Awake()
        {

        }

        public void StartSpeedRun()
        {
            _isSpeedRunStarted = true;

        }

        public void StopSpeedRun()
        {
            _isSpeedRunStarted = false;

        }

        private void Update()
        {
            if (_isSpeedRunStarted)
            {
                _currentTime += Time.deltaTime;
                OnTimeChangeEvent?.Invoke(_currentTime, _limitedTime);
            }
        }


    }
}