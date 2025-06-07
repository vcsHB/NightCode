using System;
using Combat.Casters;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{
    public class BattleAxe : MonoBehaviour
    {
        public UnityEvent OnAxeThrowEvent;
        public event Action OnTargetArriveEvent;
        public event Action OnReturnOverEvent;
        public event Action<BattleAxe> OnAxeReturnedEvent;
        [SerializeField] private float _flyDistance = 40f;
        [SerializeField] private float _flySpeed = 10f;
        [SerializeField] private float _rotationSpeed = 360f; // degrees per second
        [SerializeField] private float _returnThreshold = 0.3f;
        public bool IsAxeActive { get; private set; }
        private Caster _caster;
        private Transform _originOwnerTrm;
        private Transform _visualTrm;

        private Vector2 _startPosition;
        private Vector2 _direction;
        private bool _isReturning = false;
        private bool _isFlying = false;

        private void Awake()
        {
            _caster = GetComponentInChildren<Caster>();
            _visualTrm = transform.Find("Visual");
        }

        public void Initialize(Transform owner)
        {
            _originOwnerTrm = owner;
        }

        public void SetActive(bool value)
        {
            IsAxeActive = value;
            gameObject.SetActive(value);
        }

        public void Throw(Vector2 direction)
        {
            _direction = direction.normalized;
            _startPosition = transform.position;
            SetActive(true);
            OnAxeThrowEvent?.Invoke();
            _isFlying = true;
            _isReturning = false;
        }

        private void Update()
        {
            if (!_isFlying) return;

            _visualTrm.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
            CastAxe();
            if (!_isReturning)
            {
                transform.position += (Vector3)(_direction * _flySpeed * Time.deltaTime);

                float distance = Vector2.Distance(_startPosition, transform.position);
                if (distance >= _flyDistance)
                {
                    _isReturning = true;
                    OnTargetArriveEvent?.Invoke();
                }

            }
            else
            {
                Vector2 returnDir = (Vector2)_originOwnerTrm.position - (Vector2)transform.position;
                transform.position += (Vector3)(returnDir.normalized * _flySpeed * Time.deltaTime);

                if (returnDir.magnitude < _returnThreshold)
                {
                    _isFlying = false;
                    _isReturning = false;
                    transform.position = _originOwnerTrm.position;
                    SetActive(false);
                    OnAxeReturnedEvent?.Invoke(this);
                    OnReturnOverEvent?.Invoke();
                }
            }
        }

        private void CastAxe()
        {
            _caster.Cast();
        }
    }
}
