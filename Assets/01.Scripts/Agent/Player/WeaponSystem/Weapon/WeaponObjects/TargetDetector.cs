using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{
    public class TargetDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _targetDetectRadius;
        private Collider2D _targetCollider;

        public Collider2D DetectTarget()
        {
            _targetCollider = Physics2D.OverlapCircle(transform.position, _targetDetectRadius, _targetLayer);
            return _targetCollider;
        }

        public Collider2D[] DetectAllTargets()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _targetDetectRadius, _targetLayer);
            return colliders;

        }
        public void DetectTargetsSorted(out Collider2D[] targets)
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, _targetDetectRadius, _targetLayer);

            targets = hits
                .OrderBy(hit => Vector2.SqrMagnitude(hit.transform.position - transform.position))
                .ToArray();
        }

        public Collider2D[] DetectTargetsSorted()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, _targetDetectRadius, _targetLayer);

            var sortedHits = hits
                .OrderBy(hit => Vector2.SqrMagnitude(hit.transform.position - transform.position))
                .ToArray();

            return sortedHits;
        }

        public Collider2D DetectClosestTarget()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _targetDetectRadius, _targetLayer);

            Collider2D closest = null;
            float minDistance = float.MaxValue;

            Vector2 currentPos = transform.position;
            foreach (var collider in colliders)
            {
                float distance = Vector2.Distance(currentPos, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = collider;
                }
            }

            return closest;
        }
    }
}
