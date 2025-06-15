using UnityEngine;
namespace Agents.Enemies.Drones
{

    public class DroneAttackController : MonoBehaviour, IAgentComponent
    {
        private Drone _owner;
        public void AfterInit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(Agent agent)
        {
            _owner = agent as Drone;
            
        }
    }
}