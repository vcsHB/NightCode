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
        AutoRecovery = 5
    }
}