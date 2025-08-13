using System;
using System.Collections;
using Combat.PlayerTagSystem;
using InputManage;
using ObjectManage.GimmickObjects.Logics;
using QuestSystem.LevelSystem.SpeedRun;
using UnityEngine;
using UnityEngine.Events;

namespace SpeedRun
{

    public class SpeedRunManager : MonoBehaviour
    {
        public event Action<float> OnSpeedRunOverEvent;
        public UnityEvent OnSpeedRunCompleteEvent;
        public UnityEvent OnSpeedRunResetEvent;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] TimerUI _timerUI;
        [SerializeField] private CountDownTextObject _countDownText;
        [SerializeField] private Door _door;

        [SerializeField] private SpeedRunTimeController _timeController;

        [SerializeField] private float _currentTime;
        [SerializeField] private Transform _playerTrm;
        [SerializeField] private Transform _resetPoint;
        private bool _isComplete;
        [SerializeField] private bool _isStarted;
        [SerializeField] private SpeedRunDataController _dataController;

        

        private void Awake()
        {
            _timeController.OnTimeChangeEvent += HandleTimeChanged;
            _playerInput.SetDisableAllStatus();
        }

        private void OnDestroy()
        {

            _playerInput.SetEnabledAllStatus();
        }

        public void SetEnableInput()
        {
            _playerInput.SetEnabledAllStatus();
        }

        private void Start()
        {

        }
        public void ResetPlayerPosition()
        {
            _playerManager.CurrentPlayerTrm.position = _resetPoint.position;
            OnSpeedRunResetEvent?.Invoke();
            _timerUI.HandleRefreshTimer(0f);
            _timeController.ResetTimer();
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
            yield return new WaitForSeconds(5f);
            _countDownText.SetCountDownText("...");
            yield return new WaitForSeconds(5f);
            _countDownText.SetCountDownText("why don't go?");
            yield return waitForSec;
            yield return new WaitForSeconds(2f);
            _countDownText.SetEnable(false);

        }

        public void Complete()
        {
            _isComplete = true;
            _isStarted = false;
            _timeController.StopSpeedRun();
            _dataController.RecordSpeedRunTime(_timeController.CurrentTime);
            OnSpeedRunOverEvent?.Invoke(_timeController.CurrentTime);
            OnSpeedRunCompleteEvent?.Invoke();
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