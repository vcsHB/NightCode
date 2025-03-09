using Agents;
using UnityEngine;

namespace Combat.Casters
{
    public class FacingDirectionKnockbackCaster : KnockbackCaster
    {   
        [SerializeField] private AgentRenderer _renderer;
        [SerializeField] private float _power;

        public override void Cast(Collider2D target)
        {
            
            if (_knockbackData == null) return;
            _knockbackData.powerDirection = new Vector2(_renderer.FacingDirection * _power, 0f);
            base.Cast(target);
        }
    }


}