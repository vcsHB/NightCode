using UnityEngine;
namespace Agents.Enemies.Drones
{

    public class DroneEnemyState
    {
        protected Drone _owner;
        protected DroneStateMachine _stateMachine;

        public DroneEnemyState(Drone owner, DroneStateMachine stateMachine)
        {
            _owner = owner;
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }
 
        public virtual void UpdateState()
        {

        }

        public virtual void Exit()
        {

        }
    }
}