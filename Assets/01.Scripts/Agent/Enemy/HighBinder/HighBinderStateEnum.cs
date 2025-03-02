using Unity.Behavior;

namespace Agents.Enemies.Highbinders
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