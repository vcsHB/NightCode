using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class TargetDetector : MonoBehaviour
    {
        private Collider2D _targetCollider;

        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _targetDetectRadius;

        public Collider2D DetectTarget()
        {
            _targetCollider = Physics2D.OverlapCircle(transform.position, _targetDetectRadius, _targetLayer);
            return _targetCollider;
        }
    }
}