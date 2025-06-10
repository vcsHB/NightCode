using Agents;
using Combat;

namespace EffectSystem
{
    public class EffectBurn : EffectState
    {
        
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            ReduceStack(); 
            _ownerHealth.ApplyDamage(new CombatData()
            {
                damage = level,
                type = AttackType.Heat
                
            });
        }

        protected override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Burn;
        }
    }
}