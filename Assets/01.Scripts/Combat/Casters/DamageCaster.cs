using UnityEngine;
namespace Combat
{
    public class DamageCasterData : CasterData
    { 
        public float damage;

    }

    public class DamageCaster : MonoBehaviour, ICastable
    {
        [SerializeField] protected float _damage;

        public virtual void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IDamageable hit))
            {
                CombatData data = new CombatData()
                {
                    damage = _damage,
                    originPosition = transform.position
                };
                hit.ApplyDamage(data);
            }
        }

        public void HandleSetData(CasterData data)
        {
            DamageCasterData damageCasterData = data as DamageCasterData;
            if (damageCasterData == null) return;
            _damage = damageCasterData.damage;
        }
    }
}