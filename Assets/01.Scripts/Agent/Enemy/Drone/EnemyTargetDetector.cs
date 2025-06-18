using UnityEngine;

namespace Agents.Enemies
{
    public class EnemyTargetDetector : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _detectRadius = 5f;
        [SerializeField] private LayerMask _obstacleLayer;

        private Transform _detectedTarget;

        /// <summary>
        /// 타겟이 탐지 가능하면 true를 반환
        /// </summary>
        public bool IsTargetVisible(out Transform target)
        {
            target = null;

            // 감지 반경 내 모든 콜라이더 검색
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectRadius, _targetLayer);
            foreach (var hit in hits)
            {
                Vector2 dir = (hit.transform.position - transform.position).normalized;
                float dist = Vector2.Distance(transform.position, hit.transform.position);

                // 장애물이 없는지 체크
                RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, dir, dist, _obstacleLayer);
                if (!obstacleHit.collider)
                {
                    target = hit.transform;
                    _detectedTarget = target;
                    return true;
                }
            }

            _detectedTarget = null;
            return false;
        }

        /// <summary>
        /// 감지 반경 및 타겟 방향을 시각화
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);

            if (_detectedTarget != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, _detectedTarget.position);
            }
        }
    }
}