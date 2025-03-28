using System;
using System.Collections;
using DG.Tweening;
using SubWeaponSystem;
using UnityEngine;
namespace Combat.SubWeaponSystem.WepaonObjects
{

    public class ThrowingKnife : ProjectileSubWeaponObject
    {
        private Transform _visualTrm;
        private Vector2 _direction;
        [SerializeField] private LayerMask _blockingTargetLayer;
        [SerializeField] private float _blockingDetectLength = 2f;
        [SerializeField] private float _boundPower = 4f;
        [SerializeField] private float _blockedRotateSpeed = 1f;
        [SerializeField] private float _boundDuration = 1f;
        [SerializeField] private float _blockedLifeTime = 5f;
        private bool _isBlocked;

        protected override void Awake()
        {
            base.Awake();
            _visualTrm = transform.Find("Visual");
            _caster.OnCastSuccessEvent.AddListener(HandleHitDestroy);
        }


        private void Update()
        {
            if (IsActive)
            {
                RaycastHit2D target = Physics2D.Raycast(transform.position, _direction, _blockingDetectLength, _blockingTargetLayer);
                if (target.collider != null)
                {
                    SetBlocked();
                    _isActive = false;
                    return;
                }
                _caster.Cast();
            }
            else if (_isBlocked)
            {
                _visualTrm.localRotation = Quaternion.Euler(0, 0, _visualTrm.eulerAngles.z + _blockedRotateSpeed * Time.deltaTime * 100f);
            }
        }
        // [ContextMenu("Blocked")]
        // private void Blocked()
        // {
        //     _isBlocked = true;
        //     _rigid.linearVelocity = Vector2.zero;
        //     float randomDirection = Random.Range(-3f, 3f);
        //     _visualTrm.localPosition = Vector2.zero;
        //     _visualTrm.localRotation = Quaternion.identity;
        //     _visualTrm.DOLocalJump(Vector2.zero + new Vector2(randomDirection, 0), _boundPower, 1, _boundDuration).OnComplete(() => _isBlocked = false);
        // }
        private void SetBlocked()
        {
            _rigid.linearVelocity = Vector2.zero;
            StartCoroutine(BlockedCoroutine());
        }

        private IEnumerator BlockedCoroutine()
        {
            yield return new WaitForSeconds(_blockedLifeTime);
            Destroy();
        }
        public override void UseWeapon(SubWeaponControlData data)
        {
            _direction = data.direction;
            _visualTrm.right = _direction;
            SetVelocity(_direction * data.speed);

        }
        private void HandleHitDestroy()
        {
            Destroy();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_visualTrm == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _visualTrm.right * _blockingDetectLength);
        }
#endif

    }
}