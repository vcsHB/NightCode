using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace CameraControllers
{
    public struct ShakeRequest
    {
        public float power;
        public float duration;
        public float remainingTime;

        public ShakeRequest(float power, float duration)
        {
            this.power = power;
            this.duration = duration;
            this.remainingTime = duration;
        }
    }
    public class CameraShakeController : MonoBehaviour, ICameraControlable
    {
        private CinemachineCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _shaker;

        private bool _isShaking;
        private Coroutine _currentShakingCoroutine;

        private readonly Queue<ShakeRequest> _shakeQueue = new();

        private ShakeRequest _currentShake;
        private float _currentShakeElapsed = 0f;

        public void Initialize(CinemachineCamera camera)
        {
            _virtualCamera = camera;
            _shaker = _virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        #region External Fuctions

        /// <summary>
        /// Apply Shaking State In Waiting Queue
        /// </summary>
        /// <param name="power"></param>
        /// <param name="duration"></param>
        public void Shake(float power, float duration)
        {
            //Debug.Log("Start Shake- power: " + power + " Duration: " + duration);
            EnqueueShake(new ShakeRequest(power, duration));
        }
        /// <summary>
        /// Cancel Only Current Shaking State
        /// </summary>
        public void CancelCurrentShake()
        {
            if (_currentShakingCoroutine != null)
            {
                StopCoroutine(_currentShakingCoroutine);
                _currentShakingCoroutine = null;
            }
            SetShake(0);
            _isShaking = false;
        }
        /// <summary>
        /// Stop Shaking and Clear All Shaking Scedules
        /// </summary>
        public void StopShake()
        {
            if (_currentShakingCoroutine != null)
                StopCoroutine(_currentShakingCoroutine);
            SetShake(0);
            _isShaking = false;
            _shakeQueue.Clear();
        }


        #endregion



        private void StartShake(ShakeRequest request)
        {
            _currentShake = request;
            _isShaking = true;
            _currentShakingCoroutine = StartCoroutine(ShakeCoroutine(request));
        }

        private IEnumerator ShakeCoroutine(ShakeRequest request)
        {
            SetShake(request.power);
            _currentShakeElapsed = 0f;

            while (_currentShakeElapsed < request.remainingTime)
            {
                _currentShakeElapsed += Time.deltaTime;
                yield return null;
            }

            SetShake(0);
            _isShaking = false;
            _currentShakingCoroutine = null;

            if (_shakeQueue.Count > 0)
            {
                var next = DequeueHighestPower();
                StartShake(next);
            }
        }

        #region  Shake Queue Manage
        private void EnqueueShake(ShakeRequest request)
        {
            if (_currentShakingCoroutine == null)
            {
                StartShake(request);
                return;
            }

            // If it comes in at higher power: Stop, Prioritize processing
            if (request.power > _currentShake.power)
            {
                float remaining = _currentShake.remainingTime - _currentShakeElapsed;
                if (remaining > 0.01f)
                {
                    var resumedShake = new ShakeRequest(_currentShake.power, _currentShake.duration)
                    {
                        remainingTime = remaining
                    };
                    _shakeQueue.Enqueue(resumedShake);
                }

                StopCoroutine(_currentShakingCoroutine);
                StartShake(request);
                return;
            }

            // if new power is almost the same as currently running → Only extend Duration
            if (Mathf.Approximately(request.power, _currentShake.power))
            {
                _currentShake.remainingTime += request.duration;
                return;
            }

            // SamePower return
            bool merged = false;
            var newQueue = new Queue<ShakeRequest>();
            while (_shakeQueue.Count > 0)
            {
                var queued = _shakeQueue.Dequeue();
                if (!merged && Mathf.Approximately(queued.power, request.power))
                {
                    queued.remainingTime += request.duration;
                    merged = true;
                }
                newQueue.Enqueue(queued);
            }

            if (!merged)
            {
                newQueue.Enqueue(request);
            }

            // Replace old queue
            _shakeQueue.Clear();
            foreach (var req in newQueue)
                _shakeQueue.Enqueue(req);
        }
        private ShakeRequest DequeueHighestPower()
        {
            ShakeRequest highest = _shakeQueue.Dequeue();
            var tempList = new List<ShakeRequest>();

            while (_shakeQueue.Count > 0)
            {
                var current = _shakeQueue.Dequeue();
                if (current.power > highest.power)
                {
                    tempList.Add(highest);
                    highest = current;
                }
                else
                {
                    tempList.Add(current);
                }
            }

            foreach (var request in tempList)
                _shakeQueue.Enqueue(request);

            return highest;
        }

        #endregion

        private void SetShake(float power)
        {
            _shaker.AmplitudeGain = power;
            _shaker.FrequencyGain = power;
        }


    }
}
