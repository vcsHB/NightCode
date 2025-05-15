using Agents;
using UnityEngine;
namespace InteractSystem
{

    public class InteractController : InteractTrigger, IAgentComponent
    {
        protected Agent _owner;
        public virtual void Initialize(Agent agent)
        {
            _owner = agent;

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        protected virtual void Update()
        {
            DetectTarget();
        }

    }
}