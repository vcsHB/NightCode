using System.Collections;
using ObjectManage.Rope;
using UnityEngine;

namespace Agents.Players
{

    public class AimController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private AimGroupController _aimGroupController;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _targetLayer;


        [Header("Setting Values")]
        [SerializeField] private float _castRadius = 0.4f;
        [SerializeField] private float _shootRadius = 12f;
        [SerializeField] private float _shootCooltime = 0.2f;
        [SerializeField] private float _wireClampedDistance = 12f;
        [SerializeField] private float _clampDuration = 0.2f;
        private float _currentShootTime = 0;
        private Coroutine _clampCoroutine;
        public bool canShoot = true;
        private Player _player;
        private bool _isShoot = false;
        public bool IsShoot => _isShoot;
        private PlayerMovement _playerMovement;
        private LineRenderer _lineRenderer;

        private Vector2 _targetPoint;
        private bool _isTargeted;
        public Vector2 HangingDirection { get; private set; }


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _playerMovement = _player.GetCompo<PlayerMovement>();
            _lineRenderer = GetComponent<LineRenderer>();
        }
        public void SetAimGroup(AimGroupController aimGroup)
        {
            _aimGroupController = aimGroup;
        }

        public void AfterInit() { }
        public void Dispose() { }

        private void Update()
        {
            _currentShootTime += Time.deltaTime;
            if (_isShoot)
                HangingDirection = _aimGroupController.AnchorPos - (Vector2)transform.position;
            Vector2 mousePos = _player.PlayerInput.MouseWorldPosition;
            _aimGroupController.SetAimMarkPosition(mousePos);
            Vector2 dir = (mousePos - (Vector2)transform.position);
            RaycastHit2D boxHit = Physics2D.CircleCast(transform.position, _castRadius, dir, _shootRadius, _wallLayer | _targetLayer);
            if (boxHit.collider == null)
            {
                _isTargeted = false;
                _aimGroupController.SetVirtualAim(false);
                _lineRenderer.enabled = false;
                return;
            }
            _isTargeted = true;
            _aimGroupController.SetVirtualAim(true);
            _targetPoint = boxHit.point;

            RefreshLine();

        }

        private void RefreshLine()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _targetPoint);

            _aimGroupController.SetVirtualAimPosition(_targetPoint);
        }

        public bool Shoot()
        {
            if (_currentShootTime < _shootCooltime) return false;

            if (!_isTargeted) return false;
            _currentShootTime = 0f;
            //_playerController.turboCount = 1;
            _aimGroupController.SetActiveWire(true);
            Vector2 playerPos = _player.transform.position;
            float distance = (_targetPoint - playerPos).magnitude;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            _aimGroupController.Wire.SetWireEnable(true, _targetPoint, distance);
            if (distance > _wireClampedDistance)
            {
                _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("ShootClamping"));
                _clampCoroutine = StartCoroutine(DistanceClampCoroutine(Vector2.Lerp(playerPos, _targetPoint, (distance - _wireClampedDistance) / distance)));
            } // 마우스 위치 시차로 인해 로프 역방향으로 발사되는거 막아야됨 
            _isShoot = true;
            return true;
        }
        private IEnumerator DistanceClampCoroutine(Vector2 clampPosition)
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
            _aimGroupController.Wire.SetWireEnable(true, _targetPoint, _wireClampedDistance);
            _playerMovement.AddForceToEntity(velocity);
        }

        public void RemoveWire()
        {
            if (_clampCoroutine != null)
                StopCoroutine(_clampCoroutine);
            Vector2 velocity = _playerMovement.Velocity;
            _aimGroupController.SetActiveWire(false);
            _aimGroupController.SetAnchorPosition(transform.position);
            _aimGroupController.Wire.SetWireEnable(false);
            _isShoot = false;

            _playerMovement.SetVelocity(velocity);


        }


    }

}