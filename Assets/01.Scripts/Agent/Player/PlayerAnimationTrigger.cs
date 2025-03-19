using System;
using Agents.Animate;
using UnityEngine.Events;
namespace Agents.Players
{

    public abstract class PlayerAnimationTrigger : AnimationTrigger
    {
        public UnityEvent OnGroundPullAttackEvent;

       

#region Old Prototype Attack System Functions

        // public UnityEvent OnFirstAttackEvent;
        // public UnityEvent OnSecondAttackEvent;
        // public UnityEvent OnThirdAttackEvent;

        [Obsolete("Old Type Attack Handler")]
        public void HandleAttack1()
        {
            //OnFirstAttackEvent?.Invoke();
        }
        [Obsolete("Old Type Attack Handler")]
        public void HandleAttack2()
        {
            //OnSecondAttackEvent?.Invoke();
        }
        [Obsolete("Old Type Attack Handler")]
        public void HandleAttack3()
        {
            //OnSecondAttackEvent?.Invoke();
        }

        // public abstract void HandleAirAttack1();
        // public abstract void HandleAirAttack2();
#endregion
        public void HandleGroundPullAttack()
        {
            OnGroundPullAttackEvent?.Invoke();
        }


    }
}