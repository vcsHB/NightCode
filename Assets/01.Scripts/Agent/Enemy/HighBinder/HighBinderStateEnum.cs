using Unity.Behavior;

namespace Agents.Enemies
{
    
    [BlackboardEnum]
    public enum HighbinderStateEnum
    {
        IDLE,
        PATROL,
        CHASE,
        COMBAT,
        STUN
    }
}