using System;
using UnityEngine;
using UnityEngine.Events;
namespace Combat.Casters
{
    public class DamageCasterData : CasterData
    {
        public float damage;

    }

    public class DamageCaster : MonoBehaviour, ICastable
    {
        public UnityEvent OnCastSuccessEvent;
        public event Action<CombatData> OnCastCombatDataEvent;
        [SerializeField] protected float _damage;
        [SerializeField] protected AttackType _attackType;
        [SerializeField] private bool _invalidityResistance;

        public virtual void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IDamageable hit))
            {
                CombatData data = new CombatData()
                {
                    type = _attackType,
                    damage = _damage,
                    damageDirection = target.transform.position - transform.position,
                    originPosition = transform.position,
                    invalidityResistance = _invalidityResistance

                };
                if (hit.ApplyDamage(data))
                {
                    OnCastCombatDataEvent?.Invoke(data);
                    OnCastSuccessEvent?.Invoke();
                }
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