using Unity.Behavior;

namespace Agents.Enemies
{
    
    [BlackboardEnum]
    public enum HighBinderStateEnum
    {
        IDLE,
        PATROL,
        COMBAT,
        STUN
    }
}