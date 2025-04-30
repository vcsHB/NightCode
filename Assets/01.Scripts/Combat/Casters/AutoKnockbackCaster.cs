using UnityEngine;
namespace Combat.Casters
{

    public class AutoKnockbackCaster : KnockbackCaster
    {
        [SerializeField] private float _knockBackPower = 3f;
        [SerializeField] private Vector2 _directionOffset;

        public override void Cast(Collider2D target)
        {
            Vector2 mainDirection = (target.transform.position - transform.position).normalized;
            _knockbackData.powerDirection = (mainDirection + _directionOffset).normalized * _knockBackPower;
            base.Cast(target);
        }
    }
}