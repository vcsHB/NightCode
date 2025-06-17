using Agents.Players.ChipsetSystem.ChipsetObjects;
using Combat.PlayerTagSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class MicroDroneOwner : ChipsetFunction
    {
        [SerializeField] private MicroDrone _dronePrefab;
        [SerializeField] private int _droneAmount = 1;
        private MicroDrone[] _drones;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            _drones = new MicroDrone[_droneAmount];
            for (int i = 0; i < _droneAmount; i++)
            {
                MicroDrone newDrone = Instantiate(_dronePrefab, owner.transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);
                newDrone.SetFollowTarget(owner.transform);
                _drones[i] = newDrone;
            }
        }
    }
}