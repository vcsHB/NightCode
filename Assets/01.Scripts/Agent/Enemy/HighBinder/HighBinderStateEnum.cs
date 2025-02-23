using Unity.Behavior;

namespace Agents.Enemies
{
    
    [BlackboardEnum]
    public enum HighBinderStateEnum
    {
        IDLE,
        PATROL,
        CHASE,
        COMBAT,
        STUN
    }
}