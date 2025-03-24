using System;
using UnityEngine;
namespace Combat.Casters
{
    public class DamageCasterData : CasterData
    { 
        public float damage;

    }

    public class DamageCaster : MonoBehaviour, ICastable
    {
        public event Action<CombatData> OnCastCombatDataEvent;
        [SerializeField] protected float _damage;
        [SerializeField] protected AttackType _attackType;

        public virtual void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IDamageable hit))
            {
                CombatData data = new CombatData()
                {
                    type = _attackType,
                    damage = _damage,
                    damageDirection = target.transform.position - transform.position, 
                    originPosition = transform.position
                    
                };
                if(hit.ApplyDamage(data))
                    OnCastCombatDataEvent?.Invoke(data);
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