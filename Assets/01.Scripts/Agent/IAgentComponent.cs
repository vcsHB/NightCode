namespace Agents
{
    public interface IAgentComponent
    {
        public void Initialize(Agent agent);
        public void AfterInit();
        public void Dispose();
    }
}