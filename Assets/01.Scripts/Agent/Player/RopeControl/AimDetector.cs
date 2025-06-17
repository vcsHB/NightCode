using System;
using Combat;
using UnityEngine;
namespace Agents.Players
{
    // Aiming Data Group Structure
    public struct AimData
    {
        public bool isTargeted;
        public Vector2 mousePosition;
        public Vector2 targetPosition;
        public Vector2 originPlayerPosition;
        public float distance;
        public float distanceToPoint;
        public Transform targetTrm;
        public Vector2 aimDirection;

    }

    public struct GrabData
    {
        public bool isTargeted;
        public IGrabable grabTarget;

    }


    public class AimDetector : MonoBehaviour, IAgentComponent
    {
        public event Action<AimData> OnAimEvent;
        public event Action<GrabData> OnGrabEvent;

        [SerializeField] private float _castRadius = 0.4f;
        [SerializeField] private float _shootRadius = 12f;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _magnetLayer;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _ignoreLayer;

        private Player _player;


        // Data Properties
        private bool _isTargeted;
        private Vector2 _mousePos;
        private Vector2 _playerPos;
        private Vector2 _targetPos;
        private Transform _targetTrm;
        private Vector2 _direction;

        // Grab Data
        private IGrabable _grabTarget;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }

        public void AfterInit() { }
        public void Dispose() { }

        private void FixedUpdate()
        {
            _playerPos = _player.transform.position;
            _mousePos = Camera.main.ScreenToWorldPoint(_player.PlayerInput.MousePosition);
            _direction = _mousePos - (Vector2)transform.position;

            _isTargeted = false;
            _targetTrm = null;

            float maxDistance = _shootRadius;

            // 레이캐스트
            RaycastHit2D ignoreHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, maxDistance, _ignoreLayer);
            RaycastHit2D wallHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, maxDistance, _wallLayer);
            RaycastHit2D magnetHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, maxDistance, _magnetLayer);
            RaycastHit2D targetHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, maxDistance, _targetLayer);

            // 거리 초기화
            float ignoreDist = ignoreHit.collider ? ignoreHit.distance : Mathf.Infinity;
            float wallDist = wallHit.collider ? wallHit.distance : Mathf.Infinity;
            float magnetDist = magnetHit.collider ? magnetHit.distance : Mathf.Infinity;
            float targetDist = targetHit.collider ? targetHit.distance : Mathf.Infinity;

            // 가장 가까운 hit 찾기
            float closest = Mathf.Min(ignoreDist, wallDist, magnetDist, targetDist);

            if (closest == ignoreDist)
            {
                ClearTarget(); // 무시
            }
            else if (closest == wallDist)
            {
                _grabTarget?.OnAimExited();
                _grabTarget = null;

                _isTargeted = true;
                _targetTrm = wallHit.transform;
                _targetPos = wallHit.point;
            }
            else if (closest == magnetDist && magnetHit.collider.TryGetComponent(out IGrabable grabTarget))
            {
                if (_grabTarget != grabTarget)
                {
                    _grabTarget?.OnAimExited();
                    grabTarget.OnAimEntered();
                }

                _grabTarget = grabTarget;
                _isTargeted = true;
                _targetTrm = grabTarget.GetTransform;
                _targetPos = grabTarget.GetTransform.position;
            }
            else if (closest == targetDist)
            {
                _grabTarget?.OnAimExited();
                _grabTarget = null;

                _isTargeted = true;
                _targetTrm = targetHit.transform;
                _targetPos = targetHit.point;
            }
            else
            {
                ClearTarget(); // 아무것도 없음
            }

            InvokeGrabDataEvent();
            InvokeAimDataEvent();
        }

        private void ClearTarget()
        {
            if (_grabTarget != null)
            {
                _grabTarget.OnAimExited();
                _grabTarget = null;
            }

            _isTargeted = false;
            _targetTrm = null;
        }

        private void InvokeAimDataEvent()
        {
            OnAimEvent?.Invoke(new AimData
            {
                mousePosition = _mousePos,
                isTargeted = _isTargeted,
                originPlayerPosition = _playerPos,
                targetPosition = _targetPos,
                targetTrm = _targetTrm,
                aimDirection = _direction,
                distance = _direction.magnitude,
                distanceToPoint = (_targetPos - _playerPos).magnitude
            });
        }

        private void InvokeGrabDataEvent()
        {
            OnGrabEvent?.Invoke(new GrabData
            {
                isTargeted = _grabTarget != null,
                grabTarget = _grabTarget
            });
        }

    }
}