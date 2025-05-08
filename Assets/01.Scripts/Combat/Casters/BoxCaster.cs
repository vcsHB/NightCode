using UnityEngine;
namespace Combat.Casters
{

    public class BoxCaster : Caster
    {
        [SerializeField] private Vector2 _boxSize;
        [SerializeField] private bool _applyLocalAngle;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            float angle = 0;
            if (_applyLocalAngle)
                angle = transform.localEulerAngles.z;
            _hits = Physics2D.OverlapBoxAll(CenterPosition, _boxSize, angle, _targetLayer);
            ForceCast(_hits);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireCube(CenterPosition, _boxSize);
        }
#endif
    }
}