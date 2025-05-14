using Unity.Behavior;

namespace Agents.Enemies.Bat
{
    [BlackboardEnum]
    public enum BatStateEnum
    {
        Idle,
        Move,
        Landing,
        Attack,
        Dead
    }
}