using UnityEngine;
namespace Combat
{


    public class CircleCaster : Caster
    {
        [SerializeField] private float _detectRadius = 1f;
        public override void Cast()
        {
            base.Cast();
            _hits = Physics2D.OverlapCircleAll(CenterPosition, _detectRadius, _targetLayer);
            for (int i = 0; i < _hits.Length; i++)
            {
                for (int j = 0; j < _casters.Length; j++)
                {
                    _casters[j].Cast(_hits[i]);
                    OnCastSuccessEvent?.Invoke();

                }
            }
        }

        private void OnDrawGizmos() {
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CenterPosition, _detectRadius);
        }
    }
}