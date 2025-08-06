using System;
using UnityEngine;
namespace QuestSystem.LevelSystem.SpeedRun
{

    public class SpeedRunTimecontroller : MonoBehaviour
    {
        public event Action<float, float> OnTimeChangeEvent;
        [SerializeField] private float _currentTime;

        private bool _isSpeedRunStarted;

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
            }
        }


    }
}