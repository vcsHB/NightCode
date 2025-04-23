using System;
using System.Collections;
using ObjectManage;
using ObjectManage.Rope;
using ObjectManage.VFX;
using ObjectPooling;
using UnityEngine;

namespace Agents.Players
{
    public struct ShootData
    {
        public bool isHanged;
        public bool isGrabbed;
    }
    public class AimController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private AimGroupController _aimGroupController;

        [Header("Setting Values")]
        [SerializeField] private float _shootCooltime = 0.2f;
        [SerializeField] private float _wireClampedDistance = 12f;
        [SerializeField] private float _clampDuration = 0.2f;
        [SerializeField] private float _pullClampDuration = 0.01f;
        [SerializeField] private Gradient _gradient;
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
        private GrabData _currentGrabData;
        public bool IsGrabTargeted => _currentGrabData.isTargeted;
        public bool IsTargeted => _currentAimData.isTargeted;
        public Vector2 AimDirection => _currentAimData.aimDirection;
        public Vector2 TargetPoint => _currentAimData.targetPosition;
        public Vector2 OriginPosition => _currentAimData.originPlayerPosition;
        private float _currentShootTime = 0;
        private float _anchorDistance;
        private Vector2 _anchorPosition;


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
            AimDetector aimDetector = _player.GetCompo<AimDetector>();
            aimDetector.OnAimEvent += HandleRefreshAimData;
            aimDetector.OnGrabEvent += HandelRefreshGrabData;
        }



        private void HandleRefreshAimData(AimData data)
        {
            _aimGroupController.SetAimMarkPosition(data.mousePosition);
            _aimGroupController.SetVirtualAim(data.isTargeted);
            _aimGroupController.SetVirtualAimPosition(data.targetPosition);

            _currentAimData = data;
        }
        private void HandelRefreshGrabData(GrabData data)
        {
            _currentGrabData = data;
        }

        public void Dispose() { }

        private void Update()
        {
            _currentShootTime += Time.deltaTime;
            if (_isShoot)
                RefreshHangingDirection();
        }
        public void RefreshHangingDirection()
        {
            HangingDirection = _aimGroupController.AnchorPos - (Vector2)_player.transform.position;

        }


        public ShootData Shoot()
        {
            if (_currentShootTime < _shootCooltime) return new ShootData { isGrabbed = false };
            if (!IsTargeted) return new ShootData { isGrabbed = false };

            _currentShootTime = 0f;
            //_playerController.turboCount = 1;

            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));

            HandleHang();
            _isShoot = true;
            return new ShootData { isHanged = true };
        }

        // private void HandleGrab()
        // {
        //     _aimGroupController.SetAnchorParent(_currentGrabData.grabTarget.GetTransform);
        //     _aimGroupController.SetAnchorPosition(TargetPoint);
        //     _aimGroupController.SetActiveWire(true);
        //     _aimGroupController.Wire.SetWireEnable(true);
        // }

        private void HandleHang()
        {
            _aimGroupController.SetActiveWire(true);
            _anchorPosition = _currentAimData.targetPosition; // _currentAimData.targetPosition;
            _anchorDistance = _currentAimData.distanceToPoint;
            _aimGroupController.SetAnchorPosition(TargetPoint);
            _aimGroupController.SetAnchorParent(_currentAimData.targetTrm);
            
            if (_currentAimData.distanceToPoint > _wireClampedDistance)
            {
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("ShootClamping"));
                Vector2 newPosition = GetLerpTargetPosition(_wireClampedDistance);
                _anchorDistance = Vector2.Distance(newPosition, _anchorPosition);
                _clampCoroutine = StartCoroutine(
                    DistanceClampCoroutine(
                        newPosition, _clampDuration));
            }
            else
                _aimGroupController.SetWireEnable(true, _anchorPosition, _currentAimData.distanceToPoint);
            KatanaSlashVFXPlayer vfx = PoolManager.Instance.Pop(PoolingType.RopeAnchoredVFX) as KatanaSlashVFXPlayer;
            vfx.SetGradient(_gradient);
            vfx.SlashLerp(transform.position, _anchorPosition , _anchorDistance * 0.05f);


        }
        public void RemoveWire()
        {
            if (IsClamping)
                StopCoroutine(_clampCoroutine);
            _clampCoroutine = null;
            Vector2 velocity = _playerMovement.Velocity;
            _aimGroupController.SetActiveWire(false);
            _aimGroupController.SetAnchorParent();
            _aimGroupController.SetAnchorPosition(transform.position);
            _aimGroupController.SetWireEnable(false);
            _isShoot = false;

            _playerMovement.SetVelocity(velocity);

        }

        private IEnumerator DistanceClampCoroutine(Vector2 clampPosition, float duration, Action OnComplete = null)
        {
            Vector2 velocity = _playerMovement.Velocity;
            Vector2 before = _player.transform.position;
            float currentTime = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _player.transform.position = Vector2.Lerp(before, clampPosition, currentTime / duration);
                yield return null;
            }
            _aimGroupController.SetWireEnable(true, _anchorPosition, _wireClampedDistance);
            _playerMovement.AddForceToEntity(velocity);
            OnComplete?.Invoke();

            _clampCoroutine = null;
        }

        public void RefreshOrbitVisual()
        {
            _aimGroupController.SetOrbitVisual(_player.transform.position);
        }

        public void SetOrbitVisual(bool value)
        {
            _aimGroupController.SetOrbitVisual(value);
        }


        public void HandlePull(Action OnComplete = null)
        {
            if (IsClamping || _anchorDistance < 1f) return;
            _playerMovement.StopImmediately(true);
            _clampCoroutine = StartCoroutine(DistanceClampCoroutine(
                GetLerpTargetPositionByRatio(0.9f),
                _anchorDistance * _pullClampDuration,
                OnComplete));

        }

        #region Calculate Position Functions

        private Vector2 GetLerpTargetPosition(float clampDistance)
        {
            float distance = _currentAimData.distanceToPoint;
            return Vector2.Lerp(_currentAimData.originPlayerPosition, TargetPoint, (distance - clampDistance) / distance);
        }

        private Vector2 GetLerpTargetPositionByRatio(float ratio)
        {
            return Vector2.Lerp(_currentAimData.originPlayerPosition, _aimGroupController.AnchorPos, ratio);
        }

        #endregion

    }

}