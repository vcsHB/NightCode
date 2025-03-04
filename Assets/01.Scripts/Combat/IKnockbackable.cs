using Combat.Casters;
namespace Combat
{

    public interface IKnockbackable 
    {
        public void ApplyKnockback(KnockbackCasterData knockbackData);

    }
}