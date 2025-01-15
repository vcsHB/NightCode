using UnityEngine;
namespace Combat
{


    public class DamageCaster : MonoBehaviour, ICastable
    {
        [SerializeField] protected float _damage;
        
        public virtual void Cast(Collider2D target)
        {
            if(target.TryGetComponent(out IDamageable hit))
            {
                hit.ApplyDamage(_damage);
            }
        }
    }
}