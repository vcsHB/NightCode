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
        public bool canShoot = true;
        private Player _player;
        private bool _isShoot = false;
        public bool IsShoot => _isShoot;
        private PlayerMovement _playerMovement;
        private LineRenderer _lineRenderer;

        private Vector2 _targetPoint;
        private bool _isTargeted;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _player.PlayerInput.LeftClickEvent += HandleShootAnchor;
            _playerMovement = _player.GetCompo<PlayerMovement>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
            _player.PlayerInput.LeftClickEvent -= HandleShootAnchor;
        }


        private void Update()
        {
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
            if (!_isTargeted) return;
            //_playerController.turboCount = 1;
            _player.StateMachine.ChangeState("Hang");
            _wire.gameObject.SetActive(true);
            float distance = (_targetPoint - (Vector2)transform.position).magnitude;
            _player.FeedbackChannel.RaiseEvent(new FeedbackCreateEventData("Shoot"));
            _wire.SetWireEnable(true, _targetPoint, distance);
            _isShoot = true;
            _anchorTrm.gameObject.SetActive(true);
        }

        private void RemoveWire()
        {
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