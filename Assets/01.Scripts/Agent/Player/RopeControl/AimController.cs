using System;
using System.Collections;
using ObjectManage.Rope;
using UnityEngine;

namespace Agents.Players
{

    public class AimController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private AimGroupController _aimGroupController;

        [Header("Setting Values")]
        [SerializeField] private float _shootCooltime = 0.2f;
        [SerializeField] private float _wireClampedDistance = 12f;
        [SerializeField] private float _clampDuration = 0.2f;

        // Properties
        public bool canShoot = true;
        public bool IsShoot => _isShoot;
        public Vector2 HangingDirection { get; private set; }
        private bool _isShoot = false;
        public bool IsClamping => _clampCoroutine != null;
        private Coroutine _clampCoroutine;


        private Player _player;
        private PlayerMovement _playerMovement;

        private AimData _currentAimData;
        public bool IsTargeted => _currentAimData.isTargeted;
        public Vector2 TargetPoint => _currentAimData.targetPosition;
        public Vector2 OriginPosition => _currentAimData.originPlayerPosition;
        private float _currentShootTime = 0;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _playerMovement = _player.GetCompo<PlayerMovement>();
        }
        public void SetAimGroup(AimGroupController aimGroup)
        {
            _aimGroupController = aimGroup;
        }

        public void AfterInit()
        {
            _player.GetCompo<AimDetector>().OnAimEvent += HandleRefreshAimData;
        }

        private void HandleRefreshAimData(AimData data)
        {
            _aimGroupController.SetAimMarkPosition(data.mousePosition);
            _aimGroupController.SetVirtualAim(data.isTargeted);

            _currentAimData = data;
        }

        public void Dispose() { }

        private void Update()
        {
            _currentShootTime += Time.deltaTime;
            if (_isShoot)
                HangingDirection = _currentAimData.aimDirection;
        }


        public bool Shoot()
        {
            if (_currentShootTime < _shootCooltime) return false;
            if (!IsTargeted) return false;

            _currentShootTime = 0f;
            //_playerController.turboCount = 1;
            _aimGroupController.SetActiveWire(true);
            Vector2 playerPos = _player.transform.position;
            float distance = (TargetPoint - playerPos).magnitude;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            _aimGroupController.Wire.SetWireEnable(true, TargetPoint, distance);
            if (distance > _wireClampedDistance)
            {
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("ShootClamping"));
                _clampCoroutine = StartCoroutine(
                    DistanceClampCoroutine(
                        GetLerpTargetPosition(_wireClampedDistance)));
            } // 마우스 위치 시차로 인해 로프 역방향으로 발사되는거 막아야됨 
            _isShoot = true;
            return true;
        }
        private IEnumerator DistanceClampCoroutine(Vector2 clampPosition, Action OnComplete = null)
        {
            Vector2 velocity = _playerMovement.Velocity;
            Vector2 before = _player.transform.position;
            float currentTime = 0f;
            while (currentTime < _clampDuration)
            {
                currentTime += Time.deltaTime;
                _player.transform.position = Vector2.Lerp(before, clampPosition, currentTime / _clampDuration);
                yield return null;
            }
            _aimGroupController.Wire.SetWireEnable(true, TargetPoint, _wireClampedDistance);
            _playerMovement.AddForceToEntity(velocity);
            OnComplete?.Invoke();
        }

        public void RemoveWire()
        {
            if (IsClamping)
                StopCoroutine(_clampCoroutine);
            Vector2 velocity = _playerMovement.Velocity;
            _aimGroupController.SetActiveWire(false);
            _aimGroupController.SetAnchorPosition(transform.position);
            _aimGroupController.Wire.SetWireEnable(false);
            _isShoot = false;

            _playerMovement.SetVelocity(velocity);

        }

        public void HandlePull()
        {
            if (IsClamping) return;
            _playerMovement.StopImmediately(true);
            _clampCoroutine = StartCoroutine(DistanceClampCoroutine(GetLerpTargetPositionByRatio(0.9f)));

        }

        #region Calculate Position Functions

        private Vector2 GetLerpTargetPosition(float clampDistance)
        {
            Vector2 playerPos = _player.transform.position;
            float distance = (TargetPoint - playerPos).magnitude;

            return Vector2.Lerp(playerPos, TargetPoint, (distance - clampDistance) / distance);
        }

        private Vector2 GetLerpTargetPositionByRatio(float ratio)
        {
            Vector2 playerPos = _player.transform.position;
            float distance = (TargetPoint - playerPos).magnitude;

            return Vector2.Lerp(playerPos, TargetPoint, ratio);
        }

        #endregion

    }

}