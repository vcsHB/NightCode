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

        void OnTriggerEnter2D(Collider2D collision)
        {

        }
    }
}