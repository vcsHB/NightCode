using System;
using Agents.Animate;
using UnityEngine.Events;
namespace Agents.Players
{

    public class PlayerAnimationTrigger : AnimationTrigger
    {
        public UnityEvent OnGroundPullStartEvent;
        public UnityEvent OnGroundPullArriveEvent;
        public UnityEvent OnRopeTurboEvent;
        public UnityEvent OnRopeShootEvent;
        public UnityEvent OnRopeRemoveEvent;

        public void HandleGroundPullStart()
        {
            OnGroundPullStartEvent?.Invoke();
        }
        public void HandleGroundPullArrive()
        {
            OnGroundPullArriveEvent?.Invoke();
        }

        public void HandleRopeShoot()
        {
            OnRopeShootEvent?.Invoke();
        }

        public void HandleTurboEvent()
        {
            OnRopeTurboEvent?.Invoke();
        }

        public void HandleRopeRemove()
        {
            OnRopeRemoveEvent?.Invoke();
        }


    }
}