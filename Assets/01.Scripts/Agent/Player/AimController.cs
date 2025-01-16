using System.Collections;
using ObjectManage.Rope;
using UnityEngine;

namespace Agents.Players
{

    public class AimController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private Transform _aimTrm;
        [SerializeField] private Transform _virtualAimTrm;
        [SerializeField] private Transform _anchorTrm;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private Wire _wire;


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
            _player.PlayerInput.OnShootEvent += HandleShootAnchor;
            _playerMovement = _player.GetCompo<PlayerMovement>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
            _player.PlayerInput.OnShootEvent -= HandleShootAnchor;
        }


        private void Update()
        {
            _currentShootTime += Time.time;
            if (_isShoot)
                HangingDirection = _anchorTrm.position - transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(_player.PlayerInput.MousePosition);
            _aimTrm.position = mousePos;
            Vector2 dir = (mousePos - (Vector2)transform.position);
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _shootRadius, _wallLayer | _targetLayer);
            RaycastHit2D boxHit = Physics2D.CircleCast(transform.position, _castRadius, dir, _shootRadius, _wallLayer | _targetLayer);
            if (boxHit.collider == null)
            {
                _isTargeted = false;
                _virtualAimTrm.gameObject.SetActive(false);
                _lineRenderer.enabled = false;
                return;
            }
            _isTargeted = true;
            _virtualAimTrm.gameObject.SetActive(true);
            _targetPoint = boxHit.point;

            RefreshLine();

        }

        private void RefreshLine()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _targetPoint);
            _virtualAimTrm.position = _targetPoint;
        }

        public void HandleShootAnchor(bool value)
        {
            if (value && canShoot)
                Shoot();
            else
                RemoveWire();
        }

        private void Shoot()
        {
            if (_currentShootTime < _shootCooltime) return;

            if (!_isTargeted) return;
            _currentShootTime = 0f;
            //_playerController.turboCount = 1;
            _player.StateMachine.ChangeState("Hang");
            _wire.gameObject.SetActive(true);

            Vector2 playerPos = _player.transform.position;
            float distance = (_targetPoint - playerPos).magnitude;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            if (distance > _wireClampedDistance)
            {
                _clampCoroutine = StartCoroutine(DistanceClampCoroutine(Vector2.Lerp(playerPos, _targetPoint, (distance - _wireClampedDistance) / distance)));
            }
            _isShoot = true;
            _anchorTrm.gameObject.SetActive(true);
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
            _wire.SetWireEnable(true, _targetPoint, _wireClampedDistance);
            _playerMovement.AddForceToEntity(velocity);
        }

        private void RemoveWire()
        {
            if (_clampCoroutine != null)
                StopCoroutine(_clampCoroutine);
            Vector2 velocity = _playerMovement.Velocity;
            _wire.gameObject.SetActive(false);
            _anchorTrm.position = transform.position;
            _wire.SetWireEnable(false);
            _isShoot = false;
            _anchorTrm.gameObject.SetActive(false);
            _playerMovement.SetVelocity(velocity);
            _player.StateMachine.ChangeState("Swing");

        }


    }

}