using System;
using Combat;
using UnityEngine;

namespace ObjectManage
{

    public class PipeHandle : MonoBehaviour, IGrabable
    {
        public Action<float> OnRotationEvent;
        public Transform GetTransform => transform;
        [SerializeField] private LayerMask _playerLayer;
        private float _playerDetectRadius = 100f;
        private Transform _holdPlayerTrm;
        private bool _isGrabbed;
        private float startLocalAngle = 0f;
        private float accumulatedAngle = 0f;

        private void Update()
        {
            if (!_isGrabbed || _holdPlayerTrm == null) return;

            float currentLocalAngle = GetLocalAngle();
            float delta = -Mathf.DeltaAngle(startLocalAngle, currentLocalAngle);
            accumulatedAngle += delta;
            startLocalAngle = currentLocalAngle;
            OnRotationEvent?.Invoke(delta);

        }
        private float GetLocalAngle()
        {
            Vector2 toTarget = _holdPlayerTrm.position - transform.position;
            return Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        }


        public void Grab()
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, _playerDetectRadius, _playerLayer);
            if (player == null)
            {
                Debug.LogWarning("Can't Find Player in detect Range");
                return;
            }
            _holdPlayerTrm = player.transform;
            startLocalAngle = GetLocalAngle();
            accumulatedAngle = 0f;
            _isGrabbed = true;
        }

        public void OnAimEntered()
        {
        }

        public void OnAimExited()
        {
        }

        public void Release()
        {
            _isGrabbed = false;
            float turns = accumulatedAngle / 360f;
            //Debug.Log($"총 {turns:F2} 바퀴 회전했습니다. (시계 방향 + / 반시계 방향 -)");

        }


    }

}