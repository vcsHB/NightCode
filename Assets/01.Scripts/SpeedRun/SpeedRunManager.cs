using UnityEngine;

namespace SpeedRun
{

    public class SpeedRunManager : MonoBehaviour
    {
        [SerializeField] TimerUI _timerUI;

        [SerializeField] private float _currentTime;
        private bool _isComplete;
        private bool _isStarted;


        private void Update()
        {
            if(_isComplete || !_isStarted) return;
            _currentTime += Time.deltaTime;
            _timerUI.HandleRefreshTimer(_currentTime);
        }
        public void StartTimer ()
        {
            _isStarted = true;
        }

        public void Complete()
        {
            _isComplete = true;
            _isStarted = false;
        }

        public void ResetTimer()
        {
            _isComplete = false;

        }
    }

}