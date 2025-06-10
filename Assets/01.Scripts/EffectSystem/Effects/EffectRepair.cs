using Agents;

namespace EffectSystem
{
    public class EffectRepair : EffectState
    {
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.Restore(level);
        }

        public override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Repair;
        }
    }
}