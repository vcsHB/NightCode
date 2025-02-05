using UnityEngine;
namespace Combat
{

    public class BoxCaster : Caster
    {
        [SerializeField] private Vector2 _boxSize;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            _hits = Physics2D.OverlapBoxAll(CenterPosition, _boxSize, 0, _targetLayer);
            ForceCast(_hits);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(CenterPosition, _boxSize);
        }
    }
}