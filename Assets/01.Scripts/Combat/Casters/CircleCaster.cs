using UnityEngine;
namespace Combat.Casters
{


    public class CircleCaster : Caster
    {
        [SerializeField] private float _detectRadius = 1f;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            _hits = Physics2D.OverlapCircleAll(CenterPosition, _detectRadius, _targetLayer);
            ForceCast(_hits);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
        }
    }
}