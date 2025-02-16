using Agents.Players;
using UnityEngine.Events;

namespace Agents.Animate
{
    public class KatanaPlayerAnimationTrigger : PlayerAnimationTrigger
    {
        public UnityEvent OnAirAttack1Event;
        public UnityEvent OnAirAttack2Event;

        public override void HandleAirAttack1()
        {
            OnAirAttack1Event?.Invoke();
        }

        public override void HandleAirAttack2()
        {
            OnAirAttack2Event?.Invoke();
        }
        
    }


}
