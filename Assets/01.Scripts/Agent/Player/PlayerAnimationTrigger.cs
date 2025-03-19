using Agents.Animate;
using Combat.Casters;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players
{

    public abstract class PlayerAnimationTrigger : AnimationTrigger
    {
        [SerializeField] private Caster _swingGaurdCaster;

        public void CastSwingGuard()
        {
            _swingGaurdCaster.Cast();
        }

        public UnityEvent OnFirstAttackEvent;
        public UnityEvent OnSecondAttackEvent;
        public UnityEvent OnThirdAttackEvent;
        public UnityEvent OnGroundPullAttackEvent;


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

        public void HandleGroundPullAttack()
        {
            OnGroundPullAttackEvent?.Invoke();
        }

        public abstract void HandleAirAttack1();
        public abstract void HandleAirAttack2();


    }
}