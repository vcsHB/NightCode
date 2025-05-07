using Unity.Behavior;
using UnityEngine;
namespace Agents.Enemies.BossManage
{
    [BlackboardEnum]
    public enum BurnOutStateEnum
    {
        Idle,
        Cooling,
        DestroySequence1,
        DestroySequence2,
        DestroySequence3,
        DestroySequence4,
        SteamCooling,
        DefenceSequence,
        Dead

    }
}