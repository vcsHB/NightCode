using Agents.Animate;
using UnityEngine.Events;
namespace Agents.Players
{

    public abstract class PlayerAnimationTrigger : AnimationTrigger
    {
        public UnityEvent OnFirstAttackEvent;
        public UnityEvent OnSecondAttackEvent;
        public UnityEvent OnThirdAttackEvent;
        

        public void HandleAttack1()
        {
            OnFirstAttackEvent?.Invoke();
        }

        public void HandleAttack2()
        {
            OnSecondAttackEvent?.Invoke();
        }
        public void HandleAttack3()
        {
            OnSecondAttackEvent?.Invoke();
        }

        public abstract void HandleAirAttack1();
        public abstract void HandleAirAttack2();


    }
}