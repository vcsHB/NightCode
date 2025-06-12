namespace EffectSystem
{
    public interface IEffectable
    {
        public void ApplyEffect(EffectStateTypeEnum type, int level, int stack, float percent = 1f);
    }
}