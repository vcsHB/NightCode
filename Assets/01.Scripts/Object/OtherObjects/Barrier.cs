using System.Security.Cryptography;
using Combat;
using Combat.Casters;
using DG.Tweening;
using UnityEngine;
namespace ObjectManage.OtherObjects
{

    public class Barrier : MonoBehaviour
    {

        private Transform _visualTrm;
        private Collider2D _collider;
        [SerializeField] private bool _isVertical;
        [SerializeField] private float _barrierLength;
        [SerializeField] private float _barrierWidth;
        [SerializeField] private float _disableDuration;
        [SerializeField] private CombatData _damageData;
        [SerializeField] private DamageCaster _damageCaster;
        [SerializeField] private KnockbackCaster _knockbackCaster;
        [SerializeField] private float _knockbackPower = 10f;
        [SerializeField] private bool _isActive;

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _collider = GetComponent<Collider2D>();
            BoxCollider2D collider = _collider as BoxCollider2D;
            collider.size = _isVertical ? new Vector2(_barrierWidth, _barrierLength) : new Vector2(_barrierLength, _barrierWidth);
        }

        public void SetBarrierActive(bool value)
        {
            if (_isActive == value) return;

            if (_isVertical)
                _visualTrm.DOScaleX(value ? 1f : 0f, _disableDuration).OnComplete(() => _collider.enabled = value);
            else
                _visualTrm.DOScaleY(value ? 1f : 0f, _disableDuration).OnComplete(() => _collider.enabled = value);
            _isActive = value;
        }

        [ContextMenu("DebugSetEnable")]
        public void SetEnable()
        {
            SetBarrierActive(true);
        }

        [ContextMenu("DebugSetDisable")]
        public void SetDisable()
        {
            SetBarrierActive(false);

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;
            Vector2 direction = collider.transform.position - (Vector3)collision.GetContact(0).point;
            direction.Normalize();
            direction *= _knockbackPower;
            Debug.Log(direction);
            _knockbackCaster.SetDirection(direction);
            _knockbackCaster.Cast(collider);
            _damageCaster.Cast(collider);

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _knockbackCaster.Cast(collision);
            _damageCaster.Cast(collision);
        }
    }
}