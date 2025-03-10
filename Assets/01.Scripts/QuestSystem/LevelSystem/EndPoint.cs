using UnityEngine;
using UnityEngine.Events;
namespace QuestSystem.LevelSystem
{
    public class EndPoint : MonoBehaviour
    {
        public UnityEvent OnArriveEvent;
        [Header("Essential Setting")]
        [SerializeField] private Vector2 _areaSize;
        [SerializeField] private Vector2 _areaOffset;
        [SerializeField] private LayerMask _arriveTargetLayer;
        [SerializeField] private Color _gizmosColor;

        private Vector2 DetectCenterPos => (Vector2)transform.position + _areaOffset;
        private bool _isArrive;
        [SerializeField] private bool _isActive;


        public void CheckTargetArrived()
        {
            if (_isArrive || !_isActive) return;

            Collider2D target = Physics2D.OverlapBox(DetectCenterPos, _areaSize, 0, _arriveTargetLayer);
            if (target != null)
            {
                // 도착 처리
                _isArrive = true;
                OnArriveEvent?.Invoke();
            }
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireCube(DetectCenterPos, _areaSize);
        }
#endif

    }
}