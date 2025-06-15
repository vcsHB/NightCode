using System.Collections.Generic;
namespace Agents.Enemies.Drones
{

    public class DroneStateMachine
    {
        private Drone _owner;
        private Dictionary<string, DroneEnemyState> _droneStates;
        private DroneEnemyState _currentState;
        public DroneStateMachine(Drone owner)
        {
            _owner = owner;
        }

        public void ChnageState(string droneName)
        {
            if (_droneStates.TryGetValue(droneName, out DroneEnemyState state))
            {
                _currentState.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }


    }
}