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

            bool targetUpdated = false;

            RaycastHit2D boxHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, _shootRadius, _wallLayer | _targetLayer);
            if (boxHit.collider != null)
            {
                RaycastHit2D ignoreHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, boxHit.distance, _ignoreLayer);
                if (ignoreHit.collider == null)
                {
                    _isTargeted = true;
                    _targetPos = boxHit.point;
                    _targetTrm = boxHit.collider.transform;
                    targetUpdated = true;
                }
            }
            RaycastHit2D magnetHit = Physics2D.CircleCast(transform.position, _castRadius, _direction.normalized, _shootRadius, _magnetLayer);
            if (magnetHit.collider != null && magnetHit.collider.TryGetComponent(out IGrabable grabTarget))
            {
                if (_grabTarget != grabTarget)
                {
                    _grabTarget?.OnAimExited();
                    grabTarget.OnAimEntered();
                }

                _grabTarget = grabTarget;
                _targetPos = grabTarget.GetTransform.position;
                _targetTrm = grabTarget.GetTransform;
                _isTargeted = true;
                targetUpdated = true;
            }

            if (!targetUpdated)
            {
                bool shouldClear = false;

                if (_grabTarget != null)
                {
                    if (ShouldClearTarget(_grabTarget.GetTransform.position))
                    {
                        _grabTarget.OnAimExited();
                        _grabTarget = null;
                        shouldClear = true;
                    }
                    else
                    {
                        _targetPos = _grabTarget.GetTransform.position;
                        _targetTrm = _grabTarget.GetTransform;
                        _isTargeted = true;
                    }
                }

                if (_targetTrm != null && !shouldClear)
                {
                    if (ShouldClearTarget(_targetTrm.position))
                    {
                        _targetTrm = null;
                        shouldClear = true;
                    }
                    else
                    {
                        _targetPos = _targetTrm.position;
                        _isTargeted = true;
                    }
                }

                if (shouldClear)
                {
                    _isTargeted = false;
                }
            }

            InvokeGrabDataEvent();
            InvokeAimDataEvent();
        }

        private bool ShouldClearTarget(Vector2 targetPos)
        {
            float dist = Vector2.Distance(transform.position, targetPos);
            if (dist > _shootRadius)
                return true;

            Vector2 toTargetDir = (targetPos - (Vector2)transform.position).normalized;
            float dot = Vector2.Dot(_direction.normalized, toTargetDir);

            return dot <= 0f;
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