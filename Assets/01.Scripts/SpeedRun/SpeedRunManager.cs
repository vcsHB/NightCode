using System;
using System.Collections;
using Combat.PlayerTagSystem;
using ObjectManage.GimmickObjects.Logics;
using QuestSystem.LevelSystem.SpeedRun;
using UnityEngine;

namespace SpeedRun
{

    public class SpeedRunManager : MonoBehaviour
    {
        public event Action<float> OnSpeedRunOverEvent;
        //[SerializeField] private PlayerManager _playerManager;
        [SerializeField] TimerUI _timerUI;
        [SerializeField] private CountDownTextObject _countDownText;
        [SerializeField] private Door _door;

        [SerializeField] private SpeedRunTimeController _timeController;

        [SerializeField] private float _currentTime;
        [SerializeField] private Transform _playerTrm;
        [SerializeField] private Transform _resetPoint;
        private bool _isComplete;
        [SerializeField] private bool _isStarted;



        private void Awake()
        {
            _timeController.OnTimeChangeEvent += HandleTimeChanged;
        }

        private void Start()
        {

        }

        private void HandleTimeChanged(float currentTime, float limitedTime)
        {
            _timerUI.HandleRefreshTimer(currentTime);
        }
        [ContextMenu("DebugStart")]
        public void StartTimer()
        {
            if (_isStarted) return;
            _isStarted = true;
            StartCoroutine(SpeedRunStartCoroutine());
        }

        private IEnumerator SpeedRunStartCoroutine()
        {
            _countDownText.SetEnable(true);
            WaitForSeconds waitForSec = new WaitForSeconds(1f);
            yield return waitForSec;
            _countDownText.SetCountDownText("Starting...");
            yield return waitForSec;
            _countDownText.SetCountDownText("Complete!");
            yield return waitForSec;
            for (int i = 5; i > 0; i--)
            {
                _countDownText.SetCountDownText(i.ToString());
                yield return waitForSec;
            }

            _door.Close(); // Why Close???? 
            _countDownText.SetCountDownText("START!");
            _timeController.StartSpeedRun();
            yield return new WaitForSeconds(3f);
            _countDownText.SetCountDownText("Closing...");
            yield return waitForSec;
            _countDownText.SetEnable(false);

        }

        public void Complete()
        {
            _isComplete = true;
            _isStarted = false;
            _timeController.StopSpeedRun();
            OnSpeedRunOverEvent?.Invoke(_timeController.CurrentTime);
        }

        public void ResetTimer()
        {
            _isComplete = false;
            _currentTime = 0f;
            // if (_playerTrm == null)
            //     _playerTrm = _playerManager.CurrentPlayerTrm;
            // _playerTrm.position = _resetPoint.position;
        }
    }

}