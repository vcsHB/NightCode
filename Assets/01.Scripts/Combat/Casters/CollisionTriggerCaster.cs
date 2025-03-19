using UnityEngine;

namespace Combat.Casters
{

    public class CollisionTriggerCaster : Caster
    {

        private void OnTriggerEnter2D(Collider2D target)
        {
            ForceCast(target);
        }
    }

}