using System;
using UnityEngine;
using System.Collections.Generic;
using Basement.Player;
using DG.Tweening;

namespace Basement
{
    public class Elevator : BasementObject
    {
        [SerializeField] private List<ElevatorFloorStruct> _floorStruct;
        [SerializeField] private Transform _elevatorCameraTarget; 
        [SerializeField] private BasementPlayer _player;
        [SerializeField] private float _elevatorSpeed = 0.5f;
        private float _process = 0;
        private int _currentFloor = 3;
        private int _targetFloor = 0;
        private Sequence _seq;

        public BasementPlayer Player => _player;

        private void Awake()
        {
            _floorStruct.ForEach(elevator =>
            {
                elevator.door.Init(_player, this, elevator.floor);
                elevator.button.Init(_player, elevator.door, elevator.floor);
            });
        }

        public void ChangeFloor()
        {
            //엘리베이터에서 타고, 층을 이동하고 엘리베이터에서 내리기 까지 해줌

            _player.SetSortingLayer(0);
            var prevFloor = _floorStruct.Find(floor => floor.floor == _currentFloor);
            var targetFloor = _floorStruct.Find(floor => floor.floor == _targetFloor);

            prevFloor.door.CloseDoor();
            //플레이어 레이어를 낮추고, 못움직이게 하기

            Transform targetFollow = prevFloor.floorTarget;
            int floorDiff = Mathf.Abs(_currentFloor - _targetFloor);
            CameraManager.Instance.ChangeFollow(targetFollow, floorDiff / _elevatorSpeed);

            targetFloor.door.OpenDoor();
            _player.SetSortingLayer(5);
            _currentFloor = _targetFloor;

        }

        public void SetTargetFloor(int targetFloor)
        {
            //목표 층 수를 지정했으면 엘리베이터 문이 열리게
            _targetFloor = targetFloor;
            _floorStruct.Find(floor => floor.floor == _currentFloor).door.OpenDoor();
        }

        public override void OnInteractObject()
        {
            CameraManager.Instance.Zoom(5);
        }
    }

    [Serializable]
    public struct ElevatorFloorStruct
    {
        public int floor;
        public ElevatorDoor door;
        public ElevatorButton button;
        public Transform floorTarget;
    }
}
