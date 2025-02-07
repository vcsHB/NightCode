using UnityEngine;
namespace Combat
{

    public interface IKnockbackable 
    {
        public void ApplyKnockback(KnockbackCasterData knockbackData);

    }
}