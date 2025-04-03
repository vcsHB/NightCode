using Combat.Casters;
using UnityEngine;

namespace Ingame.Gimmick
{
    public class DamageWall : Caster
    {
        [SerializeField] private KnockbackCaster _knockBackCaster;
        [SerializeField] private float _knockbackPower;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contactCount > 0)
            {
                Vector2 knockDir = 
                    (Vector2)collision.collider.transform.position
                    - collision.contacts[0].point;
                knockDir.y = 0;
                knockDir.Normalize();

                _knockBackCaster.SetDirection(knockDir * _knockbackPower);
                ForceCast(collision.collider);
            }
        }
    }
}
