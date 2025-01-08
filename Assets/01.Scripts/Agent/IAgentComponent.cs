namespace Agents
{
    interface IAgentComponent
    {
        public void Initialize(Agent agent);
        public void AfterInit();
        public void Dispose();
    }
}