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
            for (int i = 0; i < _hits.Length; i++)
            {
                for (int j = 0; j < _casters.Length; j++)
                {
                    _casters[j].Cast(_hits[i]);
                    OnCastSuccessEvent?.Invoke();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(CenterPosition, _boxSize);
        }
    }
}