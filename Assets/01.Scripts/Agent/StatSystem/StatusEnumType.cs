using System;

namespace StatSystem
{
    //[Flags]
    public enum StatusEnumType
    {
        JumpPower = 0,
        Speed = 1,
        Health = 2,
        Attack = 3,
        Healing = 4,
        AutoRecovery = 5,
        KatanaBleeding = 6,
        KatanaBleedingChance = 7,
        KatanaAcceleration = 8,
        CrecentBladeAttackRange = 9,
        CrecentBladeInvincibleTim = 10,
        CrecentBladeShockWave = 11,
        CrecentBladeShockWaveRange = 12,
        CrecentBladeShockWaveSpeed = 13,
        CrossMagazeine = 14,
        CrossAmmo = 15,
    }
}