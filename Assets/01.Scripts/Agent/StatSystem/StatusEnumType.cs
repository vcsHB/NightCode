using System;

namespace StatSystem
{
    //[Flags]
    public enum StatusEnumType
    {
        //
        JumpPower = 0,
        Speed = 1,
        Health = 2,
        Attack = 3,
        Healing = 4,
        AutoRecovery = 5,
        //안 스킬
        KatanaBleeding = 6,
        KatanaBleedingChance = 7,
        KatanaAcceleration = 8,
        //진 레이 스킬
        CrecentBladeAttackRange = 9,
        CrecentBladeInvincibleTim = 10,
        CrecentBladeShockWave = 11,
        CrecentBladeShockWaveRange = 12,
        CrecentBladeShockWaveSpeed = 13,
        //비나 스킬
        CrossMagazeine = 14,
        CrossAmmo = 15,
    }
}