using UnityEngine;

namespace Combat.Casters
{
    public class CollisionCaster : Caster
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            ForceCast(collision.collider);
        }
    }
}
