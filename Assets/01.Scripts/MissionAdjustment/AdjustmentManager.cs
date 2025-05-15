using System;
using Combat.PlayerTagSystem;
using UnityEngine;

namespace MissionAdjust
{
    [Serializable]
    public struct TimePointScale
    {
        public float time;// Less than time.
        public int point;
    }
    public class AdjustmentManager : MonoSingleton<AdjustmentManager>
    {
        public event Action OnEndMissionEvent;
        [Header("Point Setting")]
        [SerializeField] private TimePointScale[] _timePointScales;
        [SerializeField] private int _playerAlivePoint = 10;
        [SerializeField] private int _minPoint;
        private float _startTime;
        public float CurrentTime => Time.time - _startTime;
        private float _endTime = 0f;
        public float EndTime => _endTime;
        private int _retireAmount = 0;
        [Header("Adjustment Setting & Others")]
        [SerializeField] private int _currentPoint;
        public int CurrentPoint => _currentPoint;
        [SerializeField] private bool _startTimeCountOnAwake;

        protected override void Awake()
        {
            base.Awake();
            _startTime = Time.time;
            if (_startTimeCountOnAwake)
                StartMission();
        }

        private void Start()
        {
            PlayerManager.Instance.OnPlayerDieEvent.AddListener(HandlePlayerDie);
        }

        private void HandlePlayerDie()
        {
            _retireAmount++;
        }

        public void StartMission()
        {
            _startTime = Time.time;
        }

        [ContextMenu("DebugClear")]
        public void ClearMission()
        {
            _endTime = CurrentTime;
            _currentPoint = GetTotalPoint();
            OnEndMissionEvent?.Invoke();
        }

        public int GetTotalPoint()
        {
            return GetPlayerAlivePoint() + GetTimeScalePoint();
        }

        public bool UsePoint(int amount)
        {
            if (_currentPoint >= amount)
            {
                _currentPoint -= amount;
                return true;
            }
            else
                return false;
        }

        #region Calculate Point Functions


        private int GetPlayerAlivePoint()
        {
            return (3 - _retireAmount) * _playerAlivePoint;
        }
        private int GetTimeScalePoint()
        {
            int point = 0;
            for (int i = 0; i < _timePointScales.Length; i++)
            {
                if (_timePointScales[i].time >= _endTime)
                {
                    point = _timePointScales[i].point;
                }
                else
                    break;
            }
            if (point == 0)
                point = _minPoint;

            return point;
        }

        #endregion
    }

}