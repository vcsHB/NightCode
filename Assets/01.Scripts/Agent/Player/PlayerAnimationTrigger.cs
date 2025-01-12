using System;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players
{

    public class PlayerAnimationTrigger : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnFirstAttackEvent;
        public UnityEvent OnSecondAttackEvent;
        public UnityEvent OnThirdAttackEvent;
        public event Action OnAnimationEnd;


        public void Initialize(Agent agent)
        {
        }
        public void AfterInit()
        {
        }

        public void AnimationEndTrigger()
        {
            OnAnimationEnd?.Invoke();
        }

        public void Dispose()
        {
        }

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


    }
}