using System;
using UnityEngine;
namespace Agents.Animate
{

    public class AnimationTrigger : MonoBehaviour, IAgentComponent
    {
        public virtual void Initialize(Agent agent)
        {
        }
        public void AfterInit() { }
        public void Dispose() { }

        public event Action OnAnimationEnd;



        public void AnimationEndTrigger()
        {
            OnAnimationEnd?.Invoke();
        }

    }
}