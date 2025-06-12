namespace EffectSystem
{
    public class EnemyEffectController : AgentEffectController
    {

        public override void ApplyEffect(EffectStateTypeEnum type, int level, int stack, float percent = 1)
        {
            if (type == 0) return;
            base.ApplyEffect(type, level, stack, percent);
        }

    }

}