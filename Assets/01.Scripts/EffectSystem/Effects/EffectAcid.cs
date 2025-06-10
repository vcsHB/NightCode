using Agents;
using Combat;

namespace EffectSystem
{
    public class EffectAcid : EffectState
    {
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.ApplyDamage(new CombatData()
            {
                damage = 2 * level,
                type = AttackType.Effect
            });
        }

        protected override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Acid;
        }
    }
}