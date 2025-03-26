using Agents;
using UnityEngine;
namespace InteractSystem
{

    public class InteractController : InteractTrigger, IAgentComponent
    {
        private Agent _owner;
        public void Initialize(Agent agent)
        {
            _owner = agent;
        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

    }
}