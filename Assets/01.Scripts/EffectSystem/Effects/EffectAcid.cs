using Agents;
using Combat;

namespace EffectSystem
{
    public class EffectAcid : EffectState
    {
        public EffectAcid(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.ApplyDamage(new CombatData()
            {
                damage = 2 * level,
                type = AttackType.Effect
            });
        }
    }
}