using UnityEngine;
namespace Combat.Casters
{

    public class CircleRayCaster : DirectionCaster
    {
        [SerializeField] private float _circeCastRadius = 0.1f;
        protected Vector2 StartPosition => (Vector2)transform.position + _offset;

        public override void Cast()
        {
            base.Cast();
            RaycastHit2D[] hit = Physics2D.CircleCastAll(StartPosition, _circeCastRadius, _direction, _detectDistance, _targetLayer);
            for (int i = 0; i < hit.Length; i++)
            {
                if (i == _targetMaxAmount)
                    return;

                ForceCast(hit[i].collider);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Vector2 startPosition = StartPosition;
            Gizmos.DrawWireSphere(startPosition, _circeCastRadius);
            Gizmos.DrawLine(startPosition, startPosition + (_direction.normalized * _detectDistance));
        }
#endif
    }
}