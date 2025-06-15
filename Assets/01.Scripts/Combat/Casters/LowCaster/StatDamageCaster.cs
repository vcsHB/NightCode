using Core.Attribute;
using StatSystem;
using UnityEngine;
namespace Combat.Casters
{

    public class StatDamageCaster : DamageCaster, IStatUsable
    {
        [SerializeField, ReadOnly] private AgentStatus _agentStatus;
        private StatSO damageStat;
        private float _totalDamage;

        public void Initialize(AgentStatus statCompo)
        {
            _agentStatus = statCompo;
            damageStat = _agentStatus.GetStat(StatusEnumType.Attack);
            damageStat.OnValuechange += HandleDamageStatChanged;
            _totalDamage = _damage + damageStat.Value;
        }

        private void HandleDamageStatChanged(StatSO stat, float currentValue, float prevValue)
        {
            _totalDamage = _damage + currentValue;
        }

        public override void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IDamageable hit))
            {
                CombatData data = new CombatData()
                {
                    type = _attackType,
                    damage = _totalDamage,
                    damageDirection = target.transform.position - transform.position,
                    originPosition = transform.position,
                    invalidityResistance = _invalidityResistance

                };
                if (hit.ApplyDamage(data))
                {
                    InvokeCastEvent(ref data);
                }
            }
        }


    }
}