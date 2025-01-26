using System;
using UnityEngine;
using System.Collections.Generic;
using Basement.Player;

namespace Basement
{
    public class Elevator : BasementObject
    {
        [SerializeField] private List<ElevatorFloorStruct> _floorStruct;
        [SerializeField] private BasementPlayer _player;
        private int _currentFloor = 3;
        private int _targetFloor = 0;

        public BasementPlayer Player => _player;

        private void Awake()
        {
            _floorStruct.ForEach(elevator =>
            {
                elevator.door.Init(_player, elevator.floor);
                elevator.button.Init(_player, elevator.door, elevator.floor);
            });
        }

        public void SelectTargetFloor(int floor)
        {
            _targetFloor = floor;
        }

        public void SetTargetFloor(int targetFloor) 
            => _targetFloor = targetFloor;

        public override void OnInteractObject()
        {
            CameraManager.Instance.Zoom(5);
        }
        public override void OnDeInteractObject()
        {
            _player.RemoveInteractAction();
        }
    }

    [Serializable]
    public struct ElevatorFloorStruct
    {
        public int floor;
        public ElevatorDoor door;
        public ElevatorButton button;
    }
}
