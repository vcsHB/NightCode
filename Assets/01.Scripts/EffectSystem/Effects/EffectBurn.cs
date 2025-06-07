using Agents;
using Combat;

namespace EffectSystem
{
    public class EffectBurn : EffectState
    {
        
        public EffectBurn(Agent agent, bool isResist) : base(agent, isResist)
        {
            
        }
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _ownerHealth.ApplyDamage(new CombatData()
            {
                damage = level * 0.5f,
                type = AttackType.Heat
            });
        }
    }
}