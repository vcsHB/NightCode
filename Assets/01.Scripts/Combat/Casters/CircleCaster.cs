using UnityEngine;
namespace Combat.Casters
{


    public class CircleCaster : Caster
    {
        [SerializeField] private float _detectRadius = 1f;
        public float DetectRadius => _detectRadius;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            _hits = Physics2D.OverlapCircleAll(CenterPosition, _detectRadius, _targetLayer);
            ForceCast(_hits);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
        }

#endif
    }
}